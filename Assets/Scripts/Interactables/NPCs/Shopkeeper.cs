using System;
using UI.Shop;
using UnityEngine;

namespace Interactables.NPCs
{
    public class Shopkeeper : InteractableObject
    {
        [SerializeField] private ShopManagerUI shopInterface;
        [SerializeField] private GameObject emote;
        //Interact with shopkeeper to open shop interface.

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

            shopInterface.OpenShop();
        }
    }
}
