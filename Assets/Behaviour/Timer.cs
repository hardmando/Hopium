using System.Collections;
using UnityEngine;

namespace Behaviour
{ 
    public class Timer : MonoBehaviour
    {
        [ContextMenu("Start Timer")]
        public void StartTimer(float time)
        {
            Debug.Log("Timer Started for " + time + " seconds");
            StartCoroutine(WaitFor(time));
        }

        private void EndTimer(float time)
        {
            Debug.Log("Timer Ended after " + time + " seconds");
        }

        IEnumerator WaitFor(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            EndTimer(waitTime);
        }
    }
}