using TMPro;
using UnityEngine;

namespace Assets.Scipts.Active
{
    [RequireComponent(typeof(Rigidbody))]
    public class ActiveItem : MonoBehaviour
    {
        [SerializeField] private int _level;
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private SphereCollider _trigger;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Animator _animator;
        [field: SerializeField] public Projection Projection { get; private set; }
        [field: SerializeField] public TextMeshProUGUI LevelText { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }

        private const float _minRadius = 0.4f;
        private const float _maxRadius = 0.7f;
        private const float _maxLevelRadius = 10;
        private const float _procentZoom = 0.1f;
        private const float _magnificationFactor = 2f;
        private const float _fallRate = 1.2f;
        private const float _delayTime = 0.08f;

        private readonly int IncreaseLevelHash = Animator.StringToHash("IncreaseLevel");

        public bool IsDead;
       
        public int Level
        {
            get => _level;
            set
            {
                if (value < 0)
                    value = 0;
                _level = value;
            }
        }

        private void Start() => Projection.Hide();
        private void OnValidate()
        { 
            if (_level < 0)
                _level = 0;
            if (Radius < _minRadius)
                Radius = _minRadius;
            if (Radius > _maxRadius)
                Radius = _maxRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsDead) return;

            Rigidbody attachedRig = other.attachedRigidbody;
            if (attachedRig)
                if (attachedRig.TryGetComponent(out ActiveItem activeItem))
                    if (!activeItem.IsDead && _level == activeItem._level)
                        CollapseManager.Collapse(this, this, activeItem);
        }


        [ContextMenu("IncreaseLevel")]
        public void IncreaseLevel()
        {
            _level++;
            SetLevel(_level);
            _animator.SetTrigger(IncreaseLevelHash);

            _trigger.enabled = false;
            Invoke(nameof(EnableTrigger), _delayTime);
        }
        public virtual void SetLevel(int level)
        {
            Level = level;
            //обновляем число на шаре 
            int number = (int)Mathf.Pow(2, level + 1);
            string numberString = number.ToString();
            LevelText.text = numberString;

            Radius = Mathf.Lerp(_minRadius, _maxRadius, level / _maxLevelRadius);
            Vector3 ballScale = Vector3.one * Radius * _magnificationFactor;
            _visualTransform.localScale = ballScale;
            _collider.radius = Radius;
            _trigger.radius = Radius + _procentZoom;
        }

        public void SetupToTube()
        {
            //Выключаем физику
            _trigger.enabled = false;
            _collider.enabled = false;
            _rb.isKinematic = true;
            _rb.interpolation = RigidbodyInterpolation.None;
        }

        public void Drop()
        {
            //Делаем его физическим
            _trigger.enabled = true;
            _collider.enabled = true;
            _rb.isKinematic = false;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;

            transform.parent = null;
            _rb.velocity = Vector3.down * _fallRate;
        }
        public void Disable()
        {
            _trigger.enabled = true;
            _rb.isKinematic = true;
            _collider.enabled = false;
            IsDead = true;   
        }
        public void Die() => Destroy(gameObject);
        private void EnableTrigger() => _trigger.enabled = true;
        
    }
}
      