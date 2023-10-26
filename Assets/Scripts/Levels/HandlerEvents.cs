using Assets.Srcipts.BaseItem;
using System;
using UnityEngine;

namespace Assets.Srcipts.Levels
{
    public class HandlerEvents
    {
        public static event Func<ItemType, Vector3, int, bool> OnElementDied;
        public static event Action OnResetLoseTimer;
        public static event Action OnLose;
        public static event Action OnWin;

        public static bool ElementsDied(ItemType itemType, Vector3 position, int level = 0) =>
           (bool)OnElementDied?.Invoke(itemType, position, level);
        public static void ResetedLoseTimer() => OnResetLoseTimer?.Invoke();
        public static void Lose() => OnLose?.Invoke();
        public static void Win() => OnWin?.Invoke();
    }
}