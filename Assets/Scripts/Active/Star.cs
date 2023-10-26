using Assets.Srcipts.Passive;
using System.Collections;
using UnityEngine;

namespace Assets.Srcipts.Active
{
    public class Star : ActiveItem
    {

        [Header("-----------------Star--------------------"), Space(10)]
        [SerializeField] private float _affectRadius = 1.5f;

        [SerializeField] private GameObject _affectArea;
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private LayerMask _starMask;

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

            Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius, _starMask, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].attachedRigidbody)
                {
                    colliders[i].attachedRigidbody.TryGetComponent(out ActiveItem activeItem);
                    if (activeItem)
                        activeItem.IncreaseLevel();
                }
            }

            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}