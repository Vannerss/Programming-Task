using System;
using UnityEngine;
using Managers.Inputs;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private InputManager _input;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private static readonly int BlendX = Animator.StringToHash("BlendX");
        private static readonly int BlendY = Animator.StringToHash("BlendY");

        private void Start()
        {
            _input = InputManager.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Vector2 moveDirection = _input.GetMovementDirection();

            _animator.SetFloat(BlendX, moveDirection.x);
            _animator.SetFloat(BlendY, moveDirection.y);
            
            _rigidbody.velocity = moveDirection.normalized * moveSpeed;
        }
    }
}