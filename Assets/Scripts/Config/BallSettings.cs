using UnityEngine;

namespace Assets.Srcipts.Config
{
    [CreateAssetMenu]
    public class BallSettings : ScriptableObject
    {
        public Material[] BallMaterials;
        public Material[] BallProjectionMaterials;
    }
}