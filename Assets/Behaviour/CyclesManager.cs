using System.Collections;
using UnityEngine;

namespace Behaviour
{
    public class CyclesManager : MonoBehaviour
    {
        [SerializeField] private float receivingTime;
        [SerializeField] private float dispensingTime;
        [SerializeField] private int packagesAmount;
        private Transform _spawnPoint;
        public GameObject packagePrefab;
        private int _count;
        private static int _days = 0;

        private void Awake()
        {
            _spawnPoint = transform.Find("SpawnPoint");
        }

        public void StartCycle()
        {
            _count = 0;
            StartReceivingStage();
        }

        private void StartReceivingStage()
        {
            SpawnPackages(packagesAmount);
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
                _days++;
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

        private void SpawnPackages(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector3 offset = Random.insideUnitSphere * 3f;
                Instantiate(packagePrefab,
                    new Vector3(_spawnPoint.position.x + offset.x, _spawnPoint.position.y + offset.y, _spawnPoint.position.z + offset.z), Quaternion.identity);
            }
        }
    }
}