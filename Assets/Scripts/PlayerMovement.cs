using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    [Header("Player characteristics")] [SerializeField]
    private float _moveSpeed;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _rotationSpeed;

    private float _horizontal;
    private float _vertical;
    private Vector3 _dirVector;

    private Rigidbody _rb;
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Inputs();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_dirVector != Vector3.zero)
        {
            MoveCharacter();
        }
        
    }

    public void MoveCharacter()
    {
        _rb.velocity = Vector3.ClampMagnitude(_dirVector, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_dirVector),
            Time.fixedDeltaTime * _rotationSpeed);
    }

    public void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void Inputs()
    {
        _horizontal = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        _vertical = Input.GetAxisRaw("Vertical") * _moveSpeed;
        _dirVector = new Vector3(_horizontal, 0, _vertical);
        _animator.SetFloat(Speed, Vector3.ClampMagnitude(_dirVector, 1).magnitude);
    }
}