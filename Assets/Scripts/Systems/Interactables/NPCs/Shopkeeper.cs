using Interactables;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.Interactables.NPCs
{
    public class Shopkeeper : InteractableObject
    {
        [SerializeField] private ShopManagerUI shopInterface;
        
        //Interact with shopkeeper to open shop interface.
        protected override void Interact(bool isInteracting)
        {
            if(Vector3.Distance(this.transform.position, playerTransform.position) > interactionRange)
                return;

            shopInterface.OpenShop();
        }
    }
}
