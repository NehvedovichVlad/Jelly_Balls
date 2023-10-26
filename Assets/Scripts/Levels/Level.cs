using Assets.Srcipts.BaseItem;
using UnityEngine;

namespace Assets.Srcipts.Levels
{
    [System.Serializable]
    public struct Task 
    { 
        public ItemType ItemType;
        public int Number;
        public int Level;
    }

    public class Level: MonoBehaviour
    {
        [SerializeField] private int _numberOfBalls = 50;
        [SerializeField] private int _maxCreatedBallLevel = 1;

        [SerializeField] private Task[] _tasks;
        public int NumberOfBalls => _numberOfBalls;
        public Task[] Tasks => _tasks;
    }
}
    