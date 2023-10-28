using System;
using SOScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ShopManagerUI : MonoBehaviour
    {
        /*
         * TODO: Be able to select and item, display item info on middle panel
         * activate the buy or sell button depending if selected from player or shop.
         * be able to buy or sell, and update the inventory.
         */

        [Header("Inventory Panels")]
        [SerializeField] private GameObject playerInventoryPanel;
        [SerializeField] private InventoryUI playerInventoryUI;
        [Space, SerializeField] private GameObject shopInventoryPanel;
        [SerializeField] private InventoryUI shopInventoryUI;
        [Space, SerializeField] private GameObject infoPanel;

        [Header("UI Elements")] 
        [SerializeField] private Button closeButton;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private GameObject indicator;
        

        private Clothes _selectedClothes;

        private void Start()
        {
            buyButton.onClick.AddListener(Buy);
            sellButton.onClick.AddListener(Sell);
            closeButton.onClick.AddListener(CloseShop);
        }

        private void OnEnable()
        {
            playerInventoryUI.OnSlotSelected += PlayerInventoryUISelected;
            shopInventoryUI.OnSlotSelected += ShopInventoryUISelected;
        }

        private void OnDisable()
        {
            playerInventoryUI.OnSlotSelected -= PlayerInventoryUISelected;
            playerInventoryUI.OnSlotSelected -= ShopInventoryUISelected;
        }

        public void OpenShop()
        {
            playerInventoryPanel.SetActive(true);
            shopInventoryPanel.SetActive(true);
            infoPanel.SetActive(true);
        }

        private void CloseShop()
        {
            playerInventoryUI.gameObject.SetActive(false);
            shopInventoryUI.gameObject.SetActive(false);
            infoPanel.SetActive(false);
        }

        private void PlayerInventoryUISelected(Clothes clothe)
        {
            _selectedClothes = clothe;
            Debug.Log(_selectedClothes.name);
            sellButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }        
        private void ShopInventoryUISelected(Clothes clothe)
        {
            _selectedClothes = clothe;
            Debug.Log(_selectedClothes.name);
            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(false);
        }

        private void Buy()
        {
            //TODO: Remove money from player. Remove clothes from shop inv. Add Clothes to player inv. Refresh Inventories.
            
            var clothe = _selectedClothes;
            playerInventoryUI.clothesInventory.AddClothes(clothe);
            shopInventoryUI.clothesInventory.RemoveClothes(clothe);
            _selectedClothes = null;
            indicator.SetActive(false);
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
        }

        private void Sell()
        {
            shopInventoryUI.clothesInventory.AddClothes(_selectedClothes);
            playerInventoryUI.clothesInventory.RemoveClothes(_selectedClothes);
            _selectedClothes = null;
            indicator.SetActive(false);
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
        }
    }
}