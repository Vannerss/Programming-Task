using System;
using UnityEngine;
using Managers.Inputs;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private InputManager _input;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _input = InputManager.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 moveDirection = _input.GetMovementDirection();
            
            _rigidbody.velocity = moveDirection.normalized * moveSpeed;
        }
    }
}