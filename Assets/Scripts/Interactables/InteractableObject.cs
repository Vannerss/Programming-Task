using System;
using Managers.Inputs;
using UnityEngine;

namespace Interactables
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] protected float interactionRange;
        [SerializeField] protected Transform playerTransform;

        //Created so it runs only once on update when the range was achieved.
        private InputManager _inputManager;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(this.transform.position, interactionRange);
        }

        protected virtual void Start()
        {
            _inputManager = InputManager.Instance;
            playerTransform = GameObject.FindWithTag("Player").transform;
            _inputManager.OnInteract += Interact;
        }

        protected abstract void Interact(bool isInteracting);
        
        private void OnDestroy()
        {
            _inputManager.OnInteract -= Interact;
        }
    }
}
