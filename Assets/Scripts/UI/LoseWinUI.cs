using Assets.Srcipts.Levels;
using Assets.Srcipts.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Srcipts.UI
{
    public class LoseWinUI : MonoBehaviour
    {
        private GameObject _winObject;
        private GameObject _loseObject;
        
        public void Initialize(GameObject winObject, GameObject loseObject)
        {
            _winObject = winObject;
            _loseObject = loseObject;
        }

        private void Start()
        {
            HandlerEvents.OnLose += Lose;
            HandlerEvents.OnWin += Win;
        }

        private void OnDestroy()
        {
            HandlerEvents.OnLose -= Lose;
            HandlerEvents.OnWin -= Win;
        }

        public void Win()
        {
            _winObject.SetActive(true);
            int curentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            Progress.Instance.SetLevel(curentLevelIndex + 1);
            Progress.Instance.AddCoins(50);
        }
        public void Lose() => _loseObject.SetActive(true);
        public void ToMainMenu() => SceneManager.LoadScene(0);
        public void NextLevel() => SceneManager.LoadScene(Progress.Instance.Level);
        public void Replay() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}  