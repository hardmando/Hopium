using UnityEngine;

namespace Behaviour
{
    public class GameplayLoop : MonoBehaviour
    {
        private CyclesManager _cyclesManager;
        private int[] _packageIDs;

        private void Awake()
        {
            _cyclesManager = GetComponent<CyclesManager>();
        }
        
        [ContextMenu("Start Loop")]
        private void StartLoop()
        {
            _cyclesManager.StartCycle();
            EndLoop();
        }
        private void EndLoop() => Debug.Log("End Loop");
    }
}
