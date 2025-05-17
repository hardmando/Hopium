using UnityEngine;

namespace Behaviour.Player
{
    public class RaycastAim : MonoBehaviour
    {
        private Camera _camera; 
        [SerializeField] private float maxDistance;
        private GameObject _focusedObject;
        void Start()
        {
            _camera = Camera.main;
        }

        // TODO: Check if ray is out of interactable object and call OutOfFocus()
        void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, maxDistance))
            {
                Debug.DrawRay(transform.position, _camera.transform.forward * hit.distance, Color.yellow);
                _focusedObject = hit.collider.gameObject;
                if (_focusedObject.GetComponent<IPickable>() != null)
                {
                    var interactableObject = _focusedObject;
                    interactableObject.GetComponent<IPickable>().OnFocus();
                    Debug.Log(interactableObject.name);
                }
                
            }
        }

        public GameObject GetFocusedObject()
        {
            return _focusedObject;
        }
    }
}