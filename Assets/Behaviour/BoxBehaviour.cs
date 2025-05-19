using UnityEngine;

namespace Behaviour
{
    public class BoxBehaviour : MonoBehaviour, IPickable
    {
        private static int _packageID;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private Rigidbody _rigidbody;
        private Renderer _renderer;
        private bool _isPicked;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
            _packageID = Random.Range(0, int.MaxValue);
        }
        public int GetPackageID() => _packageID;
        public void OnFocus()
        {
            _renderer.material.SetColor(EmissionColor, Color.red * .5f);
            _renderer.material.EnableKeyword("_EMISSION");
        }

        public void OutOfFocus()
        {
            _renderer.material.SetColor(EmissionColor, Color.black);
            _renderer.material.EnableKeyword("_EMISSION");
        }

        public void PickUp(Transform holdPoint)
        {
            _isPicked = true;
            _renderer.material.SetColor(EmissionColor, Color.yellow * .5f);
            _renderer.material.EnableKeyword("_EMISSION");
            
            transform.position = holdPoint.position;
            transform.parent = holdPoint;
            transform.localRotation = new Quaternion(0, 0, 0, 1);
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            _rigidbody.angularDamping = 7f;
            //_rigidbody.isKinematic = true;
        }

        public void Drop()
        {
            _isPicked = false;
            transform.parent = null;
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.angularDamping = 1f;
            //_rigidbody.isKinematic = false;
        }

        public void Throw(Vector3 throwVector)
        {
            _isPicked = false;
            transform.parent = null;
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.angularDamping = 1f;
            //_rigidbody.isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddForce(throwVector * 10, ForceMode.Impulse);
        }

        public bool IsPicked()
        {
            return _isPicked; 
        }
    }
}
