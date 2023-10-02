using TMPro;
using UnityEngine;

namespace Assets.Scipts.Active
{
    public class Ball : ActiveItem
    {
        [SerializeField] private int _level;
        public int Level 
        {
            get => _level;
            set 
            { 
                if(value < 0)
                    value = 0;
                _level = value;
            }
        }
        [SerializeField] private float _radius;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private SphereCollider _trigger;

        private const float _minRadius = 0.4f;
        private const float _maxRadius = 0.7f;
        private const float _maxLevelRadius = 10;
        private const float _procentZoom = 0.1f;
        private const float _magnificationFactor = 2f;
        private void OnValidate()
        {
            if(_level < 0)
                _level = 0;
            if(_radius < _minRadius)
                _radius = _minRadius;
            if(_radius > _maxRadius)
                _radius = _maxRadius;
        }

        [ContextMenu("IncreaseLevel")]
        public void IncreaseLevel()
        {
            _level++;
            SetLevel(_level);
        }
        public void SetLevel(int level)
        {
            Level = level;
            //обновляем число на шаре 
            int number = (int)Mathf.Pow(2, level + 1);
            string numberString = number.ToString();
            _levelText.text = numberString;

            _radius = Mathf.Lerp(_minRadius, _maxRadius, level / _maxLevelRadius);
            Vector3 ballScale = Vector3.one * _radius * _magnificationFactor;
            _visualTransform.localScale = ballScale;
            _collider.radius = _radius;
            _trigger.radius = _radius + _procentZoom;
        }
    }
}