using Managers.Inputs;
using Player;
using SOScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    //TODO: MAKE THE SHOP BE ABLE TO BE CLOSED WITH ESC KEY.
    public class ShopManagerUI : MonoBehaviour
    {
        [Header("Inventory Panels")]
        [SerializeField] private GameObject playerInventoryPanel;
        [SerializeField] private InventoryUI playerInventoryUI;
        [Space, SerializeField] private GameObject shopInventoryPanel;
        [SerializeField] private InventoryUI shopInventoryUI;
        [Space, SerializeField] private GameObject infoPanel;

        [Header("UI Elements")] 
        [SerializeField] private Image clothePreview;
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI costLabel;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private GameObject indicator;
        
        private Clothes _selectedClothes;
        private PlayerManager _playerManager;
        private InputManager _inputManager;

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _inputManager = InputManager.Instance;
            //Subscribing methods to corresponding button clicks.
            buyButton.onClick.AddListener(Buy);
            sellButton.onClick.AddListener(Sell);
            closeButton.onClick.AddListener(CloseShop);
        }

        #region Open & Close

        /// <summary>
        /// Open the shop interface.
        /// </summary>
        public void OpenShop()
        {
            playerInventoryUI.OnSlotSelected += PlayerInventoryUISelected;
            shopInventoryUI.OnSlotSelected += ShopInventoryUISelected;
            _selectedClothes = null;
            indicator.SetActive(false);
            costLabel.text = "0";
            playerInventoryPanel.SetActive(true);
            shopInventoryPanel.SetActive(true);
            infoPanel.SetActive(true);
            
            _inputManager.DisableMovement();
        }

        private void CloseShop()
        {
            playerInventoryUI.OnSlotSelected -= PlayerInventoryUISelected;
            playerInventoryUI.OnSlotSelected -= ShopInventoryUISelected;
            playerInventoryPanel.SetActive(false);
            shopInventoryPanel.SetActive(false);
            infoPanel.SetActive(false);
            indicator.SetActive(false);
            
            _inputManager.EnableMovement();
        }

        #endregion

        #region InventorionSelections

        //Handles selection from player's inventory.
        private void PlayerInventoryUISelected(Clothes clothe)
        {
            _selectedClothes = clothe;
            costLabel.text = _selectedClothes.clotheCost.ToString();

            sellButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);

            clothePreview.gameObject.SetActive(true);
            clothePreview.sprite = _selectedClothes.clotheBodySprite;
        }
        
        //Handles selection from shop's inventory.
        private void ShopInventoryUISelected(Clothes clothe)
        {
            _selectedClothes = clothe;
            costLabel.text = _selectedClothes.baseClotheCost.ToString();

            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(false);

            clothePreview.gameObject.SetActive(true);
            clothePreview.sprite = _selectedClothes.clotheBodySprite;
        }

        #endregion

        #region Buy & Sell

        //Buys the selected clothing item.
        private void Buy()
        {
            if (_selectedClothes.clotheCost > _playerManager.Gold)
            {
                Debug.Log("Not enough gold.");
                return;
            }
            
            var clothe = _selectedClothes;
            _playerManager.ReduceGold(clothe.baseClotheCost);
            
            //make clothes sell for cheaper after purchase.
            clothe.clotheCost -= (int)(clothe.clotheCost * 0.20f);
            
            playerInventoryUI.clothesInventory.AddClothes(clothe);
            shopInventoryUI.clothesInventory.RemoveClothes(clothe);
            
            ResetSelection();
        }

        //Sells the selected clothing item.
        private void Sell()
        {
            var clothe = _selectedClothes;
            _playerManager.AddGold(clothe.clotheCost);
            
            shopInventoryUI.clothesInventory.AddClothes(clothe);
            playerInventoryUI.clothesInventory.RemoveClothes(clothe);
            
            ResetSelection();
        }

        //Resets selection state to default (off).
        private void ResetSelection()
        {
            clothePreview.sprite = null;
            clothePreview.gameObject.SetActive(false);
            _selectedClothes = null;
            indicator.SetActive(false);
            costLabel.text = "0";
            
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
        }
        
        #endregion
    }
}