using Assets.Scipts.BaseItem;
using Assets.Scipts.Levels;
using System;
using UnityEngine;

namespace Assets.Scipts.Passive
{
    public class Stone : Item, IPassiveItem
    {
        [SerializeField] private GameObject _dieEffect;
        [SerializeField, Range(0, 2)] private int _level = 2;
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private Stone _stonePrefab;

        private const int _countMiniRock = 2;
        private const float _rockLevel2 = 1f;
        private const float _rockLevel1 = 0.7f;
        private const float _rockLevel0 = 0.45f;

        public void OnAffect()
        {
            if (_level > 0)
                for (int i = 0; i < _countMiniRock; i++)
                    CreateChildRock(_level - 1);
            else
                HandlerEvents.OnElementsDied(ItemType, transform.position);
            Die();
        }

        private void Die()
        {
            Instantiate(_dieEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        private void CreateChildRock(int level)
        {
            Stone newRock = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
            newRock.SetLevel(level);
        }

        private void SetLevel(int level)
        {
            _level = level;
            float scale = 1;
            if (level == 2)
                scale = _rockLevel2;
            else if (level == 1)
                scale = _rockLevel1;
            else if (level == 0)
                scale = _rockLevel0;

            _visualTransform.localScale = Vector3.one * scale;
        }

      
    }
}