using Assets.Srcipts.BaseItem;
using Assets.Srcipts.Levels;
using UnityEngine;

namespace Assets.Srcipts.Passive
{
    public class Barrel : Item, IPassiveItem
    {
        [SerializeField] private GameObject _barrelExplosion;
        public  void OnAffect() => Die();

        [ContextMenu("Die")]
        private void Die()
        {
            Instantiate(_barrelExplosion, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(this.gameObject);
            HandlerEvents.ElementsDied(ItemType, transform.position);
        }
    }
} 