using Assets.Scipts.Config;
using UnityEngine;
using Assets.Scipts.Active;
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Ball _ball;

    private void Awake()
    {
        _ball.Initialize(_ballSettings);
    }
}
