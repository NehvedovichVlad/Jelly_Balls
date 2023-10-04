using Assets.Scipts.Config;
using UnityEngine;
using Assets.Scipts.Active;
using Assets.Scipts.Tube;

public class EntryPoint : MonoBehaviour
{
    //[Header("------------------Ball------------------")]
    //[Space(20)]
    //[SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Ball _ball;

    [Header("------------------Creator------------------")]
    [Space(20)]
    [SerializeField] private Creator _creator;
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _layerMask;


    private void Awake()
    {
        //_ball.Initialize(_ballSettings);
        _creator.Initialize(_tube, _spawner, _ball, _rayTransform, _layerMask);
    }


}
