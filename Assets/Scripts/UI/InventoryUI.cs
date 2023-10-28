using System;
using NaughtyAttributes;
using SOScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public ClothesInventory clothesInventory;
        public GameObject indicator;
        
        public event Action<Clothes> OnSlotSelected;

        private void OnEnable()
        {
            clothesInventory.OnInventoryChanged += RefreshInventory;
            RefreshInventory();
        }

        private void OnDisable()
        {
            clothesInventory.OnInventoryChanged -= RefreshInventory;
        }

        [Button]
        private void RefreshInventory()
        {
            int index = 0;
            for (var i = 0; i < clothesInventory.clothesInventory.Count; i++)
            {
                var slot = transform.GetChild(i);
                var slotClothe = slot.GetComponent<SlotSelectionHandler>();
                var slotIcon = slot.GetChild(0).GetComponent<Image>();
                slotClothe.clothe = clothesInventory.clothesInventory[i];
                slotIcon.sprite = slotClothe.clothe.clotheSprite;
                slotIcon.gameObject.SetActive(true);
                index++;
            }

            for (var i = index; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
        
        public void ClotheSelected(Clothes clothe, Transform clotheSlot)
        {
            if (!indicator.activeSelf)
                indicator.SetActive(true);
            
            indicator.transform.SetParent(clotheSlot);
            indicator.transform.SetAsLastSibling();
            indicator.transform.localPosition = Vector3.zero;
            
            OnSlotSelected?.Invoke(clothe);
        }
    }
}