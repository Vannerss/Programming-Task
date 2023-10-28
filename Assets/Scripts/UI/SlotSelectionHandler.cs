using System;
using SOScripts;
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("CLICKED");
            if (transform.GetChild(0).gameObject.activeSelf)
                _inventoryUI.ClotheSelected(clothe, this.transform);
        }
    }
}