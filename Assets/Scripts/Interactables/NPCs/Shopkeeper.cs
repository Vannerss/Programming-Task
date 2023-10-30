using System;
using UI.Shop;
using UnityEngine;

namespace Interactables.NPCs
{
    public class Shopkeeper : InteractableObject
    {
        [Header("Interaction Emote")]
        [SerializeField] private GameObject emote;
        
        [Header("Shop Interface")]
        [SerializeField] private ShopManagerUI shopInterface;

        private void Update()
        {
            //Enable emote if player is on interaction range.
            emote.SetActive(Vector3.Distance(this.transform.position, playerTransform.position) < interactionRange);
        }

        protected override void Interact(bool isInteracting)
        {
            if(Vector3.Distance(this.transform.position, playerTransform.position) > interactionRange)
                return;

            shopInterface.OpenShop();
        }
    }
}
