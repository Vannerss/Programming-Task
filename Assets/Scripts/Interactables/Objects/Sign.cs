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

        protected override void Start()
        {
            base.Start();
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
        }
        
        public void Close()
        {
            panel.SetActive(false);
        }
    }
}