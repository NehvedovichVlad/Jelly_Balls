using UnityEngine;
namespace Assets.Scipts.Tube
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _sencentivity = 25f;
        [Tooltip("Максимальное расстояние на которое может смещаться Spawner")]
        [SerializeField] private float _maxXPosition = 2.5f;

        private float _xPoisition;
        private float _oldMouseX;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _oldMouseX = Input.mousePosition.x;

            if (Input.GetMouseButton(0))
                MoveSpawner();
        }

        private void MoveSpawner()
        {
            float delta = Input.mousePosition.x - _oldMouseX;
            _oldMouseX = Input.mousePosition.x;
            _xPoisition += delta * _sencentivity / Screen.width;
            _xPoisition = Mathf.Clamp(_xPoisition, -_maxXPosition, _maxXPosition);
            transform.position = new Vector3(_xPoisition, transform.position.y, transform.position.z);
        }
    }
}