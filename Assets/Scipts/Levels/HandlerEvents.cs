using Assets.Scipts.BaseItem;
using System;
using UnityEngine;

namespace Assets.Scipts.Levels
{
    public class HandlerEvents
    {
        public static event Func<ItemType, Vector3, int, bool> ElementDied;

        public static bool OnElementsDied(ItemType itemType, Vector3 position, int level = 0) =>
           (bool)ElementDied?.Invoke(itemType, position, level);
           
    }
}