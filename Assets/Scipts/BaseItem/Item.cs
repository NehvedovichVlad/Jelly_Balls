using UnityEngine;

namespace Assets.Scipts.BaseItem
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