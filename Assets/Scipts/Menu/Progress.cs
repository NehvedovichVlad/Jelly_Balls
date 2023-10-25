using Assets.Scipts.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scipts.Menu
{
    public class Progress : MonoBehaviour
    {
        [field: SerializeField] public int Coins { get; private set; }
        [field: SerializeField] public int Level { get; private set; }

        public static Progress Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
                Destroy(gameObject); 
        }

        public void SetLevel(int level) => Level = level;  
        public void AddCoins(int value) => Coins += value;
    }
}
