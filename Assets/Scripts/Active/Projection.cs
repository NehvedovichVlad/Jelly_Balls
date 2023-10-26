using TMPro;
using UnityEngine;
namespace Assets.Srcipts.Active
{
    public class Projection : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Transform _visualTransform;
        public void Setup(Material material, string numberText, float radius)
        {
            _renderer.material = material;
            _text.text = numberText;

            float diameter = radius * 2f;
            _visualTransform.localScale = Vector3.one * diameter;
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        public void SetPosition(Vector3 position) => transform.position = position;

    }
}