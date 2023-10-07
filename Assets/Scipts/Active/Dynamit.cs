using Assets.Scipts.Passive;
using System.Collections;
using UnityEngine;

namespace Assets.Scipts.Active
{
    public class Dynamit : ActiveItem
    {
        [Header("-----------------Dynamit--------------------"), Space(10)]
        [SerializeField] private float _affectRadius = 1.5f;
        [SerializeField] private float _forceValue = 1000f;

        [SerializeField] private GameObject _affectArea;        
        [SerializeField] private GameObject _effectPrefab;

        private void OnValidate() => 
            _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;

        protected override void Start()
        {
            base.Start();
            _affectArea.SetActive(true);
        }
        public override void DoEffect() =>
            StartCoroutine(AffectProcess());

        private IEnumerator AffectProcess()
        {
            _affectArea.SetActive(true);
            Animator.enabled = true;  
            yield return new WaitForSeconds(1f);

            Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody rigidbody = colliders[i].attachedRigidbody;
                if (rigidbody)
                {
                    Vector3 fromTo = (rigidbody.transform.position - transform.position).normalized;
                    rigidbody.AddForce(fromTo * _forceValue + Vector3.up * _forceValue * 0.5f);
    
                    rigidbody.TryGetComponent(out IPassiveItem passiveItem);
                    if (passiveItem != null)
                        passiveItem.OnAffect();
                } 
            }

            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }  
    }
}