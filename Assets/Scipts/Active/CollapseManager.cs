using Assets.Scipts.BaseItem;
using Assets.Scipts.Levels;
using Assets.Scipts.Passive;
using System.Collections;
using UnityEngine;

namespace Assets.Scipts.Active
{
    public static class CollapseManager
    {
        private const float _timeDeceleration = 0.08f;
        private const float _partRadius = 0.15f;
        private const float _differentHeight = 0.02f;

        public static void Collapse(MonoBehaviour monoBeh, ActiveItem itemA, ActiveItem itemB)
        {
            ActiveItem toItem;
            ActiveItem fromItem;
            //Если высота шаров по у отличается больше чем на _differentHeight
            var posA = itemA.transform.position.y;
            var posB = itemB.transform.position.y;
            if (Mathf.Abs(posA - posB) > _differentHeight)
            {
                if(posA > posB)
                {
                    fromItem = itemA;
                    toItem = itemB;
                }
                else
                {
                    fromItem = itemB;
                    toItem = itemA;
                }
            }
            else
            {
                //Если скорость А больше чем скорость B
                if(itemA.RigidBody.velocity.magnitude > itemB.RigidBody.velocity.magnitude)
                {
                    fromItem = itemA;
                    toItem = itemB;
                }
                else
                {
                    fromItem = itemB;
                    toItem = itemA;
                }
            }
            monoBeh.StartCoroutine(CollapseProcess(fromItem, toItem));

            HandlerEvents.OnResetedLoseTimer();
        }

        private static IEnumerator CollapseProcess(ActiveItem fromItem, ActiveItem toItem)
        {
            fromItem.Disable();

            if (fromItem.ItemType == ItemType.Ball || toItem.ItemType == ItemType.Ball)
            {
                Vector3 startPosition = fromItem.transform.position;
                for (float t = 0; t < 1f; t += Time.deltaTime / _timeDeceleration)
                {
                    fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, t);
                    yield return null;
                }
                fromItem.transform.position = toItem.transform.position;
            }
            
            if(fromItem.ItemType == ItemType.Ball && toItem.ItemType == ItemType.Ball)
            {
                fromItem.Die();
                toItem.DoEffect();
                ExplodeBall(toItem.transform.position, toItem.Radius + _partRadius);
            }
            else
            {
                if (fromItem.ItemType == ItemType.Ball)
                    fromItem.Die();
                else
                    fromItem.DoEffect();

                if(toItem.ItemType == ItemType.Ball)
                    toItem.Die();
                else
                    toItem.DoEffect();
            } // TODO: возможно переписать чисто на activeItem
        }

        private static void ExplodeBall(Vector3 position, float radius)
        {
            //Собираем в массив все коллайдеры, которые попали в сферу радиусом radius
            Collider[] colliders = Physics.OverlapSphere(position, radius);
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody rig = colliders[i].attachedRigidbody;
                colliders[i].TryGetComponent(out IPassiveItem passiveItem);
                if (rig)
                    passiveItem = rig.GetComponent<IPassiveItem>();
                if(passiveItem != null)
                    passiveItem.OnAffect();
            }
                

        }
    }
}
