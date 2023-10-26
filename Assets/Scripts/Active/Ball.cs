using Assets.Srcipts.Config;
using Assets.Srcipts.Levels;
using UnityEngine;

namespace Assets.Srcipts.Active
{
    public class Ball : ActiveItem
    {
        [Header("----------------------Ball-----------------------"), Space(10)]
        [SerializeField] private Renderer _renderer;
        [SerializeField] private BallSettings _ballSettings;
        [SerializeField] private Transform _visualTransform;

        private const float _minRadius = 0.4f;
        private const float _maxRadius = 0.7f;
        private const float _maxLevelRadius = 10;
        private const float _procentZoom = 0.1f;
        private const float _magnificationFactor = 2f;

        private void OnValidate()
        {
            if (Level < 0)
                Level = 0;
            if (Radius < _minRadius)
                Radius = _minRadius;
            if (Radius > _maxRadius)
                Radius = _maxRadius;
        }
        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            _renderer.material = _ballSettings.BallMaterials[level];

            Radius = Mathf.Lerp(_minRadius, _maxRadius, level / _maxLevelRadius);
            Vector3 ballScale = Vector3.one * Radius * _magnificationFactor;
            _visualTransform.localScale = ballScale;
            Collider.radius = Radius;
            Trigger.radius = Radius + _procentZoom;

            Projection.Setup(_ballSettings.BallProjectionMaterials[level], LevelText.text, Radius);

            if(HandlerEvents.ElementsDied(ItemType, transform.position, level))           
                Die();   
        }

        public override void DoEffect() => IncreaseLevel();     
    }
}