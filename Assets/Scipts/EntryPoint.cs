using Assets.Scipts.Config;
using UnityEngine;
using Assets.Scipts.Active;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Ball _ball;
    [SerializeField] private ActiveItem _activeItem;

    private void Start()
    {
        _ball.Initialize(_ballSettings);
    }
}
