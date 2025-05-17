using UnityEngine;

namespace Behaviour.Player
{
    public class RaycastAim : MonoBehaviour
    {
        private Camera _camera; 
        [SerializeField] private float maxDistance;
        private GameObject _focusedObject;
        private GameObject _interactableObject;
        void Start()
        {
            _camera = Camera.main;
        }

        // TODO: Check if ray is out of interactable object and call OutOfFocus()
        void FixedUpdate()
        {
            if (_interactableObject != null)
            {
                _interactableObject.GetComponent<IPickable>().OutOfFocus();
                _interactableObject = null;
            }

            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, maxDistance))
            {
                Debug.DrawRay(transform.position, _camera.transform.forward * hit.distance, Color.yellow);
                _focusedObject = hit.collider.gameObject;
                if (_focusedObject.GetComponent<IPickable>() != null)
                {
                    _interactableObject = _focusedObject;
                    _interactableObject.GetComponent<IPickable>().OnFocus();
                }
                
            }
        }

        public GameObject GetInteractableObject()
        {
            return _interactableObject;
        }
    }
}