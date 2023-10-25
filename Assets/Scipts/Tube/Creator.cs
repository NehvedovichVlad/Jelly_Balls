using Assets.Scipts.Active;
using Assets.Scipts.Levels;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scipts.Tube
{
    public class Creator : MonoBehaviour
    {
        private Transform _tube;
        private Transform _spawner;
        private Transform _rayTransform;
        private LayerMask _layerMask;

        private ActiveItem _itemInTube;
        private ActiveItem _itemInSpawner;

        private const int _minIndexBall = 0;
        private const int _maxIndexBall = 5;
        private const float _timeSetupPosition = 0.45f;
        private const float _maxDistance = 100f;

        private ActiveItemFactory _factory;
        private int _ballsLeft;
        private TextMeshProUGUI _numberOfBalls;
        public void Initialize(Transform tube, Transform spawner,  Transform rayTransform, LayerMask layerMask, TextMeshProUGUI numberOfBalls)
        {
            _tube = tube;
            _spawner = spawner;
            _rayTransform = rayTransform;
            _layerMask = layerMask;
            _numberOfBalls = numberOfBalls;
        }

        private void Start()
        {
            _ballsLeft = FindObjectOfType<Level>().NumberOfBalls;
            UpdateBallsLeftText();
            HandlerEvents.OnResetLoseTimer += ResetLoseTimer;
            HandlerEvents.OnWin += CreatorEnabled;
            HandlerEvents.OnWin += StopWaitForLose;

            _factory = new();
            CreateItemInTube();
            StartCoroutine(MoveToSpawner());
        }
        private void OnDestroy()
        {
            HandlerEvents.OnResetLoseTimer -= ResetLoseTimer;
            HandlerEvents.OnWin -= CreatorEnabled;
            HandlerEvents.OnWin -= StopWaitForLose;
        }

        private void LateUpdate()
        {
            if (_itemInSpawner)
            {
                Ray ray = new Ray(_spawner.position, Vector3.down);
                RaycastHit hit;
                if (Physics.SphereCast(ray, _itemInSpawner.Radius, out hit, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
                {
                    _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2f, hit.distance, 1f);
                    _itemInSpawner.Projection.SetPosition(_spawner.position + Vector3.down * hit.distance);
                }

                if (Input.GetMouseButtonUp(0))
                    Drop();
            }
        }

        private void CreatorEnabled() => this.gameObject.SetActive(false); 

        private void UpdateBallsLeftText() =>
            _numberOfBalls.text = _ballsLeft.ToString();
        private void CreateItemInTube()
        {
            if (_ballsLeft == 0) return;
            int itemLevel = Random.Range(_minIndexBall, _maxIndexBall);
            _itemInTube = CreateItem(ActiveItemTypes.Ball);
            _itemInTube.SetLevel(itemLevel);
            _itemInTube.SetupToTube();
            _ballsLeft--;
        }

        private IEnumerator MoveToSpawner()
        {
            _itemInTube.transform.parent =_spawner;
            for (float t = 0; t < 1f; t += Time.deltaTime / _timeSetupPosition)
            {
                _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
                yield return null;
            }
            _itemInTube.transform.localPosition = Vector3.zero;
            _itemInSpawner = _itemInTube;
            _rayTransform.gameObject.SetActive(true);
            _itemInSpawner.Projection.Show();
            _itemInTube = null;
            CreateItemInTube();
            UpdateBallsLeftText();
        }

        private Coroutine _waitForLose;
        private void Drop()
        {
            _itemInSpawner.Drop();
            _itemInSpawner.Projection.Hide();
            //Чтобы бросить мяч только один раз, обнуляем его
            _itemInSpawner = null;
            _rayTransform.gameObject.SetActive(false);
            if (_itemInTube)
                StartCoroutine(MoveToSpawner());
            else
                _waitForLose = StartCoroutine(WaitForLose());
        }

        private void ResetLoseTimer()
        {
            if (_waitForLose != null)
            {
                StopCoroutine(_waitForLose);
                _waitForLose = StartCoroutine(WaitForLose());
            }   
        }
        private void StopWaitForLose()
        {
            if (_waitForLose != null)
                StopCoroutine(_waitForLose);
        }   

        private IEnumerator WaitForLose()
        {
           for (float t = 0; t < 5f; t += Time.deltaTime)
               yield return null;

            HandlerEvents.Lose();
        }

        private ActiveItem CreateItem(ActiveItemTypes activeItemTypes) =>
            Instantiate(_factory.Get(activeItemTypes), _tube.position, Quaternion.identity);
    }
}