using Managers.Inputs;
using TMPro;
using UnityEngine;

namespace Interactables.Objects
{
    public class Sign : InteractableObject
    {
        [Header("Interaction Emote")]
        [SerializeField] private GameObject emote;
        
        [Header("Sign Panel")]
        [SerializeField] private GameObject panel;
        
        [Header("Text To Display")]
        [SerializeField, TextArea(3, 6)] private string text;
        [SerializeField] private TextMeshProUGUI signUIText;

        private InputManager _inputManager;
        
        protected override void Start()
        {
            base.Start();
            _inputManager = InputManager.Instance;
            signUIText.text = text;
        }

        private void Update()
        {
            //Enable emote if player is on interaction range.
            emote.SetActive(Vector3.Distance(this.transform.position, playerTransform.position) < interactionRange);
        }
        
        protected override void Interact(bool isInteracting)
        {
            if(Vector3.Distance(this.transform.position, playerTransform.position) > interactionRange)
                return;

            Open();
        }

        private void Open()
        {
            panel.SetActive(true);
            _inputManager.DisableMovement();
        }
        
        public void Close()
        {
            panel.SetActive(false);
            _inputManager.EnableMovement();
        }
    }
}