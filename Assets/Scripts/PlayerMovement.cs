using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    [Header("Variables")]
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 2f;

    private Vector3 _originalPosition = new Vector3(15, 1, 10);
    private Quaternion _originalRotation = new Quaternion(0, 180, 0, 0);
    private Vector3 _velocity;
    private bool _isGrounded;

    private void Start()
    {
        //_originalPosition = transform.position;
        //_originalRotation = transform.localRotation;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameState == GameManager.GameState.running)
        {
            //return;

            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = transform.right * horizontalInput + transform.forward * verticalInput;
            float magnitude = Mathf.Clamp01(movementDirection.magnitude) * _speed;
            movementDirection.Normalize();

            _controller.SimpleMove(movementDirection * magnitude);

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }

            _velocity.y += _gravity * Time.deltaTime;

            _controller.Move(_velocity * Time.deltaTime);
        }
    }
    public void ResetPosition()
    {
        Debug.Log("PRE " + transform.position);
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
        Debug.Log("POSLE " + transform.position);
    }
}
