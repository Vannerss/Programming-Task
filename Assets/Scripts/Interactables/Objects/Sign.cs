using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactables.Objects
{
    public class Sign : InteractableObject
    {
        [SerializeField, TextArea(3, 6)] private string signText;
        [SerializeField] private TextMeshProUGUI signUIText;

        protected override void Start()
        {
            base.Start();
            signUIText.text = signText;
        }

        protected override void Interact(bool isInteracting)
        {
            if(Vector3.Distance(this.transform.position, playerTransform.position) > interactionRange)
                return;

            signUIText.gameObject.SetActive(isInteracting);
        }
    }
}