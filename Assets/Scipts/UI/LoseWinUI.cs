using Assets.Scipts.Levels;
using UnityEngine;

namespace Assets.Scipts.UI
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
            HandlerEvents.Lose += Lose;
            HandlerEvents.Win += Win;
        }

        private void OnDestroy()
        {
            HandlerEvents.Lose -= Lose;
            HandlerEvents.Win -= Win;
        }

        public void Win() => _winObject.SetActive(true);
        public void Lose() => _loseObject.SetActive(true);

    }
}