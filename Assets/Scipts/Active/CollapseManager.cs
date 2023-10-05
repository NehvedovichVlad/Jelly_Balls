using System.Collections;
using UnityEngine;

namespace Assets.Scipts.Active
{
    public static class CollapseManager
    {
        private const float _timeDeceleration = 0.08f;
        public static void Collapse(MonoBehaviour monoBeh, ActiveItem itemA, ActiveItem ItemB) =>
            monoBeh.StartCoroutine(CollapseProcess(itemA, ItemB));

        private static IEnumerator CollapseProcess(ActiveItem itemA, ActiveItem itemB)
        {
            itemA.Disable();

            Vector3 startPosition = itemA.transform.position;
            for (float t = 0; t < 1f; t += Time.deltaTime / _timeDeceleration) 
            {   
                itemA.transform.position = Vector3.Lerp(startPosition, itemB.transform.position, t);
                yield return null;
            }
            itemA.transform.position = itemB.transform.position;
            itemA.Die();
            itemB.IncreaseLevel();
        }
    }
}
