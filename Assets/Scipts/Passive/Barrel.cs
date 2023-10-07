using UnityEngine;

namespace Assets.Scipts.Passive
{
    public class Barrel : MonoBehaviour, IPassiveItem
    {
        [SerializeField] private GameObject _barrelExplosion;
        public  void OnAffect() => Die();

        [ContextMenu("Die")]
        private void Die()
        {
            Instantiate(_barrelExplosion, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(this.gameObject);
        }
    }
} 