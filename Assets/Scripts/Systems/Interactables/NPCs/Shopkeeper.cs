using Interactables;
using UI;
using UnityEngine;

namespace Systems.Interactables.NPCs
{
    public class Shopkeeper : InteractableObject
    {
        [SerializeField] private ShopManagerUI shopPanel;
        
        protected override void Interact(bool isInteracting)
        {
            if(Vector3.Distance(this.transform.position, playerTransform.position) > interactionRange)
                return;

            shopPanel.OpenShop();
        }
    }
}
