using System.Collections;
using UnityEngine;

namespace Behaviour
{
    public class CyclesManager : MonoBehaviour
    {
        [SerializeField] private float receivingTime;
        [SerializeField] private float dispensingTime;
        private int _count;

        [ContextMenu("Start Cycle")]
        public void StartCycle()
        {
            _count = 0;
            StartReceivingStage();
        }

        private void StartReceivingStage()
        {
            StartCoroutine(WaitReceive());
        }

        private void StartDispensingStage()
        {
            StartCoroutine(WaitDispense());
        }

        private void EndReceivingStage()
        {
            StartDispensingStage();
        }

        private void EndDispensingStage()
        {
            if (_count < 3)
            {
                StartReceivingStage();
            }
            else
            {
                Debug.Log("Cycle Ended!");
            }
        }

        IEnumerator WaitReceive()
        {
            yield return new WaitForSeconds(receivingTime);
            EndReceivingStage();
        }

        IEnumerator WaitDispense()
        {
            yield return new WaitForSeconds(dispensingTime);
            _count++;
            EndDispensingStage();
        }
    }
}