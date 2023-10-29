using TMPro;
using UnityEngine;

namespace Interactables.Objects
{
    public class Sign : InteractableObject
    {
        [SerializeField] private TextMeshProUGUI signUIText;
        [SerializeField] private GameObject emote;

        
        private void Update()
        {
            if (Vector3.Distance(this.transform.position, playerTransform.position) < interactionRange)
            {
                emote.SetActive(true);
            }
            else
            {
                emote.SetActive(false);
            }
        }
        
        protected override void Interact(bool isInteracting)
        {
            if(Vector3.Distance(this.transform.position, playerTransform.position) > interactionRange)
                return;

            //TODO: CREATE THE SIGN TEXT
            
            //signUIText.gameObject.SetActive(isInteracting);
        }
    }
}