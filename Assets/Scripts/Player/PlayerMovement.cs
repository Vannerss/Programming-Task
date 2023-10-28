using System;
using UnityEngine;
using Managers.Inputs;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        
        private InputManager _input;
        private CharacterController _controller;
        
        private void Start()
        {
            _input = InputManager.Instance;
            _controller = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            Vector2 inputDirection = _input.GetMovementDirection();
            
            Vector3 moveDirection = new Vector3(inputDirection.x, inputDirection.y, 0f).normalized;
            _controller.Move(moveDirection * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}