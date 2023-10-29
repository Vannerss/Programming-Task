using System;
using SOScripts;
using UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class SlotSelectionHandler : MonoBehaviour, IPointerClickHandler
    {
        private InventoryUI _inventoryUI;
        public Clothes clothe;

        private void Start()
        {
            _inventoryUI = transform.parent.GetComponent<InventoryUI>();
        }

        //Handles inventory slot selection. When slot image is clicked.
        public void OnPointerClick(PointerEventData eventData)
        {
            if (transform.GetChild(0).gameObject.activeSelf)
                _inventoryUI.ClotheSelected(clothe, this.transform);
        }
    }
}