using Unity.VisualScripting;
using UnityEngine;

namespace Behaviour
{
    public class BoxBehaviour : MonoBehaviour, IPickable
    {
        private Rigidbody _rigidbody;
        private Renderer _renderer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
            
        }
        public void OnFocus()
        {
            _renderer.material.SetColor("_EmissionColor", Color.red * .5f);
            _renderer.material.EnableKeyword("_EMISSION");
        }

        public void OutOfFocus()
        {
            _renderer.material.SetColor("_EmissionColor", Color.black);
            _renderer.material.EnableKeyword("_EMISSION");
        }

        public void PickUp(Transform holdPoint)
        {
            _renderer.material.SetColor("_EmissionColor", Color.yellow * .5f);
            _renderer.material.EnableKeyword("_EMISSION");
            
            transform.position = holdPoint.position;
            transform.parent = holdPoint;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }

        public void Drop()
        {
            transform.parent = null;
            _rigidbody.constraints = RigidbodyConstraints.None;
        }

        public void Throw()
        {
            transform.parent = null;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
        }
    }
}
