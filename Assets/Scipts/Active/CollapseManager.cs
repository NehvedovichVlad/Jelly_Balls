using UnityEngine;

namespace Assets.Scipts.Active
{
    public static class CollapseManager
    {
        public static void Collapse(ActiveItem itemA, ActiveItem ItemB)
        {
            Object.Destroy(itemA.gameObject);
            ItemB.IncreaseLevel(); 
        }
    }
}
