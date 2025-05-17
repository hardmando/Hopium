using UnityEngine;
using UnityEngine.InputSystem;
using Behaviour.Player;

namespace Behaviour.Player
{ 
    [RequireComponent(typeof(RaycastAim))]
    public class FirstPersonController : MonoBehaviour
    {
        private Camera _camera;
        private CharacterController _characterController;
        private RaycastAim _raycastAim;
        private GameObject _interactableObject;
        
        private InputAction _iaMove;
        private InputAction _iaLook;
        private InputAction _iaInteract;
        
        private Vector2 _movementVector; 
        private Vector2 _lookVector;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float cameraSensitivity;
        private float _xRotation;
        
        private void Start()
        {
            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
            _iaMove = InputSystem.actions.FindAction("Move");
            _iaLook = InputSystem.actions.FindAction("Look");   
            _iaInteract = InputSystem.actions.FindAction("Attack");
            Cursor.lockState = CursorLockMode.Locked;
            _raycastAim = GetComponent<RaycastAim>();
        }
        private void Update()
        {
            MoveCharacter();
            MoveCamera();
            if (_iaInteract.IsPressed())
            {
                PickUp();
            }
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
            
            Vector3 move = transform.right * _movementVector.x + transform.forward * _movementVector.y;
            _characterController.Move(move * (movementSpeed * Time.deltaTime));
        }

        private void PickUp()
        {
            _interactableObject = _raycastAim.GetInteractableObject();
            if (_interactableObject != null)
            {
                _interactableObject.GetComponent<IPickable>().PickUp(); 
                Debug.Log("Interact");
            }
        }
    }
}