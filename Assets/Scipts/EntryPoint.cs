using UnityEngine;
using Assets.Scipts.Active;
using Assets.Scipts.Tube;

public class EntryPoint : MonoBehaviour
{
    [Header("------------------Creator------------------")]
    [Space(20)]
    [SerializeField] private Ball _ball;
    [SerializeField] private Creator _creator;
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _layerMask;


    private void Awake()
    {
        //_ball.Initialize(_ballSettings);
        _creator.Initialize(_tube, _spawner, _rayTransform, _layerMask);
    }


}
