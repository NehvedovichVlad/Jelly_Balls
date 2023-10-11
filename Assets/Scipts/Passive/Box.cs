using Assets.Scipts.BaseItem;
using Assets.Scipts.Levels;
using System;
using UnityEngine;

namespace Assets.Scipts.Passive
{
    public class Box : Item, IPassiveItem
    {
        [SerializeField] private GameObject[] _levels;
        [SerializeField] private GameObject _breakEffectPrefab;
        [SerializeField] private Animator _animator;
        [SerializeField, Range(0,2)] private int _health = 1;

        public event Action<ItemType, Vector3> Died;
        private void Start() => SetHealth(_health);
        public void OnAffect()
        {
            _health -= 1;
            Instantiate(_breakEffectPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            _animator.SetTrigger("Shake");
            if (_health < 0)
                Die();
            else
                SetHealth(_health);
        }

        private void SetHealth(int value) 
        {
            for (int i = 0; i < _levels.Length; i++)
                _levels[i].SetActive(i <= value);
        }

        private void Die() 
        {
            Destroy(this.gameObject);
            HandlerEvents.OnElementsDied(ItemType, transform.position);
        }
    }
}