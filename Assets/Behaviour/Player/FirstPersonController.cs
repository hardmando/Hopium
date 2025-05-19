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
        private InputAction _iaZoom;
        private InputAction _iaRotate;
        
        private Vector2 _movementVector; 
        private Vector2 _lookVector;
        private Vector3 _gravity;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float cameraSensitivity;
        private float _xRotation;
        private float _zoomAmount = 0;
        private Vector3 _holdPointDefault;
        private bool _cameraControlEnabled = true;
        
        private void Start()
        {
            _gravity = Physics.gravity;
            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
            _holdPoint = GameObject.Find("HoldPoint").transform;
            _holdPointDefault = _holdPoint.localPosition;
            
            _iaMove = InputSystem.actions.FindAction("Move");
            _iaLook = InputSystem.actions.FindAction("Look");   
            _iaInteract = InputSystem.actions.FindAction("Attack");
            _iaDrop = InputSystem.actions.FindAction("Drop");
            _iaZoom = InputSystem.actions.FindAction("Zoom");   
            _iaRotate = InputSystem.actions.FindAction("Rotate");
            
            Cursor.lockState = CursorLockMode.Locked;
            _raycastAim = GetComponent<RaycastAim>();
        }
        private void Update()
        {
            MoveCharacter();
            MoveCamera();
            
            _iaInteract.performed += ctx => PickUp();
            _iaInteract.Enable();
            _iaDrop.performed += ctx => { if(_heldObject != null) _heldObject.GetComponent<IPickable>().Drop(); _heldObject = null; };
            _iaDrop.Enable();
            _iaZoom.performed += ctx => ZoomObject();
            _iaZoom.Enable();
            if (_iaRotate.inProgress) RotateObject(); else _cameraControlEnabled = true;
        }

        private void MoveCamera()
        {
            if (_cameraControlEnabled)
            {
                _lookVector = _iaLook.ReadValue<Vector2>();
                float lookY = _lookVector.y * cameraSensitivity * Time.deltaTime;
                float lookX = _lookVector.x * cameraSensitivity * Time.deltaTime;

                _xRotation -= lookY;
                _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

                _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
                transform.Rotate(Vector3.up, lookX);
            }
        }

        private void MoveCharacter()
        {
            _movementVector = _iaMove.ReadValue<Vector2>();
            
            Vector3 move = (transform.right * _movementVector.x + transform.forward * _movementVector.y) + _gravity;
            _characterController.Move(move * (movementSpeed * Time.deltaTime));
        }

        private void RotateObject()
        {
            if (_heldObject)
            {
                _cameraControlEnabled = false;
                Vector3 rotationMovement = Pointer.current.delta.ReadValue();
                _heldObject.transform.Rotate(rotationMovement * .2f, Space.World);
            }
        }

        private void PickUp()
        {
            _zoomAmount = 0;
            _holdPoint.localPosition = _holdPointDefault;
            _interactableObject = _raycastAim.GetInteractableObject();
            if (_heldObject)
            {
                _heldObject.GetComponent<IPickable>().Throw(_camera.transform.forward);
                _heldObject = null;
            }
            else
            {
                if (_interactableObject)
                {
                    if (_interactableObject.GetComponent<IPickable>().IsPicked() != true && _heldObject == null)
                    {
                        _interactableObject.GetComponent<IPickable>().PickUp(_holdPoint);
                        _heldObject = _interactableObject;
                    }
                }
            }
        }

        private void ZoomObject()
        {
            float zoom = _iaZoom.ReadValue<Vector2>().y;
            if (_heldObject && zoom != 0)
            {
                if (_zoomAmount is >= 0 and < 10 || _zoomAmount <= 0 && zoom > 0 || _zoomAmount >= 10 && zoom < 0)
                {
                    _zoomAmount += zoom;
                    _holdPoint.transform.localPosition += new Vector3(0, 0, zoom * .1f);
                }
            }
        }
    }
}