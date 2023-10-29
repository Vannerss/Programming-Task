using System;
using System.Numerics;
using UnityEngine;
using Managers.Inputs;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        
        private InputManager _input;
        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;

        private void Start()
        {
            _input = InputManager.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            _moveDirection = _input.GetMovementDirection();
        }
        
        private void FixedUpdate()
        {
            _rigidbody.velocity = _moveDirection.normalized * moveSpeed;
        }
    }
}