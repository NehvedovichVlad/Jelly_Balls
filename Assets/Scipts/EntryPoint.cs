using UnityEngine;
using Assets.Scipts.Active;
using Assets.Scipts.Tube;
using Assets.Scipts.Levels;

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

    [Header("------------------ScoreManager------------------")]
    [Space(20)]
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Level _level;
    [SerializeField] private ScoreElement[] _scoreElementsPrefab;
    [SerializeField] private Transform _itemScoreParent;
    [SerializeField] private Camera _camera;
    private void Awake()
    {
        _creator.Initialize(_tube, _spawner, _rayTransform, _layerMask);
        _scoreManager.Initialize(_level, _scoreElementsPrefab, _itemScoreParent, _camera);
    }


}
