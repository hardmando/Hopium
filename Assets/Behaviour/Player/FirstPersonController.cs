using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviour.Player
{ 
    [RequireComponent(typeof(RaycastAim))]
    public class FirstPersonController : MonoBehaviour
    {
        private Camera _camera;
        private CharacterController _characterController;
        private Transform _holdPoint;
        private RaycastAim _raycastAim;
        private GameObject _interactableObject;
        private GameObject _heldObject;
        
        private InputAction _iaMove;
        private InputAction _iaLook;
        private InputAction _iaInteract;
        private InputAction _iaDrop;
        
        private Vector2 _movementVector; 
        private Vector2 _lookVector;
        private Vector3 _gravity;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float cameraSensitivity;
        private float _xRotation;
        
        private void Start()
        {
            _gravity = Physics.gravity;
            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
            _holdPoint = GameObject.Find("HoldPoint").transform;
            
            _iaMove = InputSystem.actions.FindAction("Move");
            _iaLook = InputSystem.actions.FindAction("Look");   
            _iaInteract = InputSystem.actions.FindAction("Attack");
            _iaDrop = InputSystem.actions.FindAction("Drop");
            
            Cursor.lockState = CursorLockMode.Locked;
            _raycastAim = GetComponent<RaycastAim>();
        }
        private void Update()
        {
            MoveCharacter();
            MoveCamera();
            
            _iaInteract.performed += ctx => PickUp();
            _iaInteract.Enable();
            _iaDrop.performed += ctx => { _heldObject.GetComponent<IPickable>().Drop(); _heldObject = null; };
            _iaDrop.Enable();
        }

        private void MoveCamera()
        {
            _lookVector = _iaLook.ReadValue<Vector2>();
            float lookY = _lookVector.y * cameraSensitivity * Time.deltaTime;
            float lookX = _lookVector.x * cameraSensitivity * Time.deltaTime;
            
            _xRotation -= lookY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.Rotate(Vector3.up, lookX);
        }

        private void MoveCharacter()
        {
            _movementVector = _iaMove.ReadValue<Vector2>();
            
            Vector3 move = (transform.right * _movementVector.x + transform.forward * _movementVector.y) + _gravity;
            _characterController.Move(move * (movementSpeed * Time.deltaTime));
        }

        private void PickUp()
        {
            _interactableObject = _raycastAim.GetInteractableObject();
            if (_heldObject != null)
            {
                _heldObject.GetComponent<IPickable>().Throw(_camera.transform.forward);
                _heldObject = null;
            }
            else
            {
                if (_interactableObject != null)
                {
                    if (_interactableObject.GetComponent<IPickable>().IsPicked() != true && _heldObject == null)
                    {
                        _interactableObject.GetComponent<IPickable>().PickUp(_holdPoint);
                        _heldObject = _interactableObject;
                        Debug.Log("Interact");
                    }
                }
            }
        }
    }
}