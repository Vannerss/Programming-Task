using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers.Inputs
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        
        private InputController _inputController;
        private InputAction _move;
        private InputAction _interact;
        private InputAction _inventory;

        public event Action<bool> OnInteract;
        public event Action<bool> OnOpenInventory;
        
        
        private void Awake()
        {
            _inputController = new InputController();
            
            //Created a singleton of the object.
            if (Instance != null)
                Destroy(this.gameObject);

            Instance = this;
        }

        private void OnEnable()
        {
            _move = _inputController.Player.Move;
            _move.Enable();

            _interact = _inputController.Player.Interact;
            _interact.started += Interact;
            _interact.canceled += Interact;
            _interact.Enable();            
            
            _inventory = _inputController.Player.Inventory;
            _inventory.started += OpenInventory;
            _inventory.canceled += OpenInventory;
            _inventory.Enable();
        }

        private void OnDisable()
        {
            _move.Disable();
            
            _interact.started -= Interact;
            _interact.canceled -= Interact;
            _interact.Disable();
            
            _inventory.started -= OpenInventory;
            _inventory.canceled -= OpenInventory;
            _inventory.Disable();
        }

        public Vector2 GetMovementDirection()
        {
            return _move.ReadValue<Vector2>();
        }

        private void Interact(InputAction.CallbackContext context)
        {
            //if interact input was pressed, isInteracting = true, if input was released isInteract = false.
            bool isInteracting = context.started;

            OnInteract?.Invoke(isInteracting);
        }        
        
        private void OpenInventory(InputAction.CallbackContext context)
        {
            //if interact input was pressed, isInteracting = true, if input was released isInteract = false.
            bool isInteracting = context.started;

            OnOpenInventory?.Invoke(isInteracting);
        }

        public void EnableMovement() => _move.Enable();
        public void DisableMovement() => _move.Disable();
    }
}
