using Assets.Scipts.BaseItem;
using System.Collections;
using UnityEngine;

namespace Assets.Scipts.Levels
{
    public class ScoreManager: MonoBehaviour
    {
        private Level _level;
        private ScoreElement[] _scoreElementsPrefab;
        private ScoreElement[] _scoreElements;
        private Transform _itemScoreParent;
        private Camera _camera;

        public void Initialize(Level level, ScoreElement[] scoreElementPrefab, Transform itemScoreParent, Camera camera)
        {
            _level = level;
            _scoreElementsPrefab = scoreElementPrefab;
            _itemScoreParent = itemScoreParent;
            _camera = camera;

            SetupTaskForUI();
            HandlerEvents.OnElementDied += AddScore;
        }

        private void OnDestroy() => HandlerEvents.OnElementDied -= AddScore;

        public bool AddScore(ItemType itemType, Vector3 position, int level = 0)
        {
            for (int i = 0; i < _scoreElements.Length; i++)
            {
                if (_scoreElements[i].ItemType != itemType) continue;
                if (_scoreElements[i].CurrentScore == 0) continue;
                if (_scoreElements[i].Level != level) continue;

                StartCoroutine(AddScoreAnimation(_scoreElements[i], position));
                return true;
            }
            return false;
        }
        public void CheckWin()
        {
            for (int i = 0; i < _scoreElements.Length; i++)
                if (_scoreElements[i].CurrentScore != 0) return;
            HandlerEvents.Win();
        }

        private IEnumerator AddScoreAnimation(ScoreElement scoreElement, Vector3 position)
        {
            GameObject icon = Instantiate(scoreElement.FlyingIconPrefab, position, Quaternion.identity);
            Vector3 a = position;
            Vector3 b = position + Vector3.back * 6.5f + Vector3.down * 5f;
            Vector3 screenPosition = new Vector3(scoreElement.IconTransform.position.x, scoreElement.IconTransform.position.y, -_camera.transform.position.z);
            Vector3 d = _camera.ScreenToWorldPoint(screenPosition);
            Vector3 c = d + Vector3.back * 6f;

            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                icon.transform.position = Bezier.GetPoint(a, b, c, d, t);
                yield return null;
            }
            Destroy(icon.gameObject);
            scoreElement.AddOne();

            CheckWin();
        }

        private void SetupTaskForUI()
        {
            _scoreElements = new ScoreElement[_level.Tasks.Length];

            for (int taskIndex = 0; taskIndex < _level.Tasks.Length; taskIndex++)
            {
                Task task = _level.Tasks[taskIndex];
                ItemType itemType = task.ItemType;
                for (int i = 0; i < _scoreElementsPrefab.Length; i++)
                {
                    if (itemType == _scoreElementsPrefab[i].ItemType)
                    {
                        ScoreElement newScoreElement = Instantiate(_scoreElementsPrefab[i], _itemScoreParent);
                        newScoreElement.Setup(task);

                        _scoreElements[taskIndex] = newScoreElement;
                    }
                }
            }
        }
    }
}
    