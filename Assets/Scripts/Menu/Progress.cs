using Assets.Srcipts.SaveSystemFolder;
using UnityEngine;

namespace Assets.Srcipts.Menu
{
    public class Progress : MonoBehaviour
    {
        [field: SerializeField] public int Coins { get; private set; }
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public bool IsMusicOn { get; private set; }

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

            Load();
        }

        public void SetLevel(int level) 
        {
            Level = level;
            Save();
        } 
        public void AddCoins(int value) 
        {
            Coins += value;
            Save();
        }

        [ContextMenu("DeleteFile")]
        public void DeleteFile() => SaveSystem.DeleteFile();    

        [ContextMenu("Save")]
        public void Save() => SaveSystem.Save(this);

        [ContextMenu("Load")]
        public void Load()
        {
            ProgressData progressData =  SaveSystem.Load();
            if (progressData != null) 
            {
                Coins = progressData.Coins;
                Level = progressData.Level;
                IsMusicOn = progressData.IsMusicOn;
            }
            else
            {
                Coins = 0;
                Level = 1;
                IsMusicOn = true;
            }
        }
    }
} 
