using Assets.Scipts.Config;
using UnityEngine;

namespace Assets.Scipts.Active
{
    public class Ball : ActiveItem
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private BallSettings _ballSettings;

        public override void SetLevel(int level)
        {
            base.SetLevel(level);
            _renderer.material = _ballSettings.BallMaterials[level];

            Projection.Setup(_ballSettings.BallProjectionMaterials[level], LevelText.text, Radius);
        }
    }
}