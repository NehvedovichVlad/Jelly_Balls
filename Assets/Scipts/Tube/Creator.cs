using Assets.Scipts.Active;
using System.Collections;
using UnityEngine;

namespace Assets.Scipts.Tube
{
    public class Creator : MonoBehaviour
    {
        [SerializeField] private Transform _tube;
        [SerializeField] private Transform _spawner;
        [SerializeField] private ActiveItem _ballPrefab;

        private ActiveItem _itemInTube;
        private ActiveItem _itemInSpawner;

        private const int _minIndexBall = 0;
        private const int _maxIndexBall = 5;
        private const float _timeSetupPosition = 0.3f;

        private void Start()
        {
            CreateItemInTube();
            StartCoroutine(MoveToSpawner());
        }

        private void Update()
        {
            if (_itemInSpawner)
                if (Input.GetMouseButtonUp(0))
                    Drop();
        }

        private void CreateItemInTube()
        {
            // Назначаеи шару случайный уровень
            int itemLevel = Random.Range(_minIndexBall, _maxIndexBall);
            _itemInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
            _itemInTube.SetLevel(itemLevel);
            _itemInTube.SetupToTube();
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
            _itemInTube = null;
            CreateItemInTube();
        }

        private void Drop()
        {
            _itemInSpawner.Drop();
            //Чтобы бросить мяч только один раз, обнуляем его
            _itemInSpawner = null;
            if (_itemInTube)
                StartCoroutine(MoveToSpawner());
        }
         
    }
}