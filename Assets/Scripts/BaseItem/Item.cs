using UnityEngine;

namespace Assets.Srcipts.BaseItem
{
    public enum ItemType
    {
        Empty,
        Ball,
        Stone,
        Box,
        Barrel,
        Dynamit,
        Star
    }

    public class Item : MonoBehaviour
    {
        public ItemType ItemType;
    }
    
}