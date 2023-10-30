using Managers.Inputs;
using UnityEngine;

namespace Interactables
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] protected float interactionRange;
        [SerializeField] protected Transform playerTransform;
        
        private InputManager _inputManager;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(this.transform.position, interactionRange);
        }

        protected virtual void Start()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable()
        {
            _inputManager = InputManager.Instance;
            _inputManager.OnInteract += Interact;
        }
        
        private void OnDisable()
        {
            _inputManager.OnInteract -= Interact;
        }

        protected abstract void Interact(bool isInteracting);
        
    }
}
