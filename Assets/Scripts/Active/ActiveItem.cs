using Assets.Srcipts.BaseItem;
using TMPro;
using UnityEngine;

namespace Assets.Srcipts.Active
{
    [RequireComponent(typeof(Rigidbody))]
    public class ActiveItem : Item
    {
        [SerializeField] private int _level;

        [SerializeField] protected Animator Animator; // Todo разделить на классы

        [field: SerializeField] public Projection Projection { get; private set; }
        [field: SerializeField] public TextMeshProUGUI LevelText { get; private set; }

        [field: SerializeField] public float Radius { get; protected set; }
        [field: SerializeField] public SphereCollider Collider  { get; protected set; }
        [field: SerializeField] public SphereCollider Trigger { get; protected set; }
        [field: SerializeField] public Rigidbody RigidBody { get; protected set; }

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

        protected virtual void Start() => Projection.Hide();
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsDead) return;

            Rigidbody attachedRig = other.attachedRigidbody;
            if (attachedRig)
                if (attachedRig.TryGetComponent(out ActiveItem activeItem))
                    if (!activeItem.IsDead && _level == activeItem._level)
                        CollapseManager.Collapse(this, this, activeItem);
        }
        public virtual void SetLevel(int level)
        {
            Level = level;
            //обновляем число на шаре 
            int number = (int)Mathf.Pow(2, level + 1);
            string numberString = number.ToString();
            LevelText.text = numberString;
        }

        public void SetupToTube()
        {
            //Выключаем физику
            Trigger.enabled = false;
            Collider.enabled = false;
            RigidBody.isKinematic = true;
            RigidBody.interpolation = RigidbodyInterpolation.None;
        }

        public void Drop()
        {
            //Делаем его физическим
            Trigger.enabled = true;
            Collider.enabled = true;
            RigidBody.isKinematic = false;
            RigidBody.interpolation = RigidbodyInterpolation.Interpolate;

            transform.parent = null;
            RigidBody.velocity = Vector3.down * _fallRate;
        }

        [ContextMenu("IncreaseLevel")]
        public void IncreaseLevel()
        {
            Level++;
            SetLevel(Level);
            Animator.SetTrigger(IncreaseLevelHash);

            Trigger.enabled = false;
            Invoke(nameof(EnableTrigger), _delayTime);
        }
        public void Disable()
        {
            Trigger.enabled = true;
            RigidBody.isKinematic = true;
            Collider.enabled = false;
            IsDead = true;   
        }

        public virtual void DoEffect() { }
        public void Die() => Destroy(gameObject);

        private void EnableTrigger() => Trigger.enabled = true;


    }
}
      