using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputSystem.InputSettings;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] Transform _root;
    [SerializeField] Animator _animator;
    [SerializeField] float _movingThreshold;
    [SerializeField] float _speed;
    [SerializeField] float _gravity;
    [SerializeField] CharacterController _controller;
    [SerializeField] bool _followCameraOrientation;
    [SerializeField, ShowIf(nameof(_followCameraOrientation))] Camera _camera;
    [SerializeField] AudioSource _audioSource;

    private PlayerInput playerInput;
    Vector3 _direction;
    Vector3 _aimDirection;
    Vector3 _playerMovement;
    Vector3 _calculatedDirection;

    public event UnityAction<Vector3> OnMove;

    private void Start()
    {
        _moveInput.action.started += StartMove; 
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += EndMove;
    }
    private void Update()
    {
        /*Debug.Log($"{_playerMovement}");*/

        if (_followCameraOrientation)  
        {
            _controller.transform.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
        }

        if (_playerMovement.magnitude > _movingThreshold)
        {
            _animator.SetBool("Running", true); 
            _animator.SetBool("Breathing idel", false); 
            _animator.SetFloat("InputX", _playerMovement.x);
            _animator.SetFloat("InputZ", _playerMovement.z);

            var tmpDirection = (_playerMovement * _speed * Time.deltaTime);
            var forwardForCamera = _camera.transform.TransformDirection(tmpDirection);
            _calculatedDirection.x = forwardForCamera.x;
            _calculatedDirection.z = forwardForCamera.z;
        }
        else
        {
            _animator.SetBool("Running", false); 
            _animator.SetBool("Breathing idel", true); 
            _calculatedDirection.x = 0;
            _calculatedDirection.z = 0;
            _animator.SetFloat("InputX", 0);
            _animator.SetFloat("InputZ", 0);
            PlaySound();
        }

        if (_controller.isGrounded) 
        {
            _calculatedDirection.y = 0;
        }
        else
        {
            _calculatedDirection.y += _gravity * Time.deltaTime;
        }

        _controller.Move(_calculatedDirection * Time.deltaTime); 
        OnMove?.Invoke(_calculatedDirection); 
    }

    private void StartMove(InputAction.CallbackContext obj) 
    {
        var joystick = obj.ReadValue<Vector2>();
        _playerMovement = new Vector3(joystick.x, 0, joystick.y);
    }

    private void EndMove(InputAction.CallbackContext obj) 
    {

        _playerMovement = new Vector3(0, 0, 0);
    }
    public void PlaySound()
    {
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
}