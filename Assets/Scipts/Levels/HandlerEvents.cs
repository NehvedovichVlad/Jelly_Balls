using Assets.Scipts.BaseItem;
using System;
using UnityEngine;

namespace Assets.Scipts.Levels
{
    public class HandlerEvents
    {
        public static event Func<ItemType, Vector3, int, bool> ElementDied;
        public static event Action ResetLoseTimer;
        public static event Action Lose;
        public static event Action Win;

        public static bool OnElementsDied(ItemType itemType, Vector3 position, int level = 0) =>
           (bool)ElementDied?.Invoke(itemType, position, level);
        public static void OnResetedLoseTimer() => ResetLoseTimer?.Invoke();
        public static void OnLose() => Lose?.Invoke();
        public static void OnWin() => Win?.Invoke();
    }
}