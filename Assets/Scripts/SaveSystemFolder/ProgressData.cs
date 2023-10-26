using Assets.Srcipts.Menu;

namespace Assets.Srcipts.SaveSystemFolder
{
    [System.Serializable]
    public class ProgressData 
    {
        public int Coins;
        public int Level;
        public bool IsMusicOn;

        public ProgressData(Progress progress)
        {
            Coins = progress.Coins;
            Level = progress.Level;
            IsMusicOn = progress.IsMusicOn;
        }
    }
}   