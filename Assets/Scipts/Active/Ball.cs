using Assets.Scipts.Config;
using UnityEngine;

namespace Assets.Scipts.Active
{
    public class Ball : ActiveItem
    {
        [SerializeField] private BallSettings _ballSettings;
        [SerializeField] private Renderer _renderer;

        public void Initialize(BallSettings ballSettings)
        {
            _ballSettings = ballSettings;
        }

        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            
            _renderer.material = _ballSettings.BallMaterials[level];  
        }
    }
}