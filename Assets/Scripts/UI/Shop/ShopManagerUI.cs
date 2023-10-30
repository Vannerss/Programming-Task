using Managers.Inputs;
using Player;
using SOScripts;
using TMPro;
using UI.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopManagerUI : MonoBehaviour
    {
        [Header("Audio")] 
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip selectSfx;
        [SerializeField] private AudioClip buysellSfx;
        
        [Header("Player Inventory")]
        [SerializeField] private GameObject playerInventoryPanel;
        [SerializeField] private InventoryUI playerInventoryUI;
        
        [Header("Shop Inventory")]
        [SerializeField] private GameObject shopInventoryPanel;
        [SerializeField] private InventoryUI shopInventoryUI;
        
        [Header("UI Selection")]
        [SerializeField] private GameObject indicator;
        
        [Header("UI Preview Panel")] 
        [SerializeField] private GameObject previewPanel;
        [SerializeField] private Image clothePreview;
        [SerializeField] private TextMeshProUGUI costLabel;
        
        [Header("UI Buttons")]
        [SerializeField] private Button buyButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private Button closeButton;
        
        private Clothes _selectedClothe;
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
            
            ResetSelection();
            
            playerInventoryPanel.SetActive(true);
            shopInventoryPanel.SetActive(true);
            previewPanel.SetActive(true);
            
            _inputManager.DisableMovement();
        }

        private void CloseShop()
        {
            playerInventoryUI.OnSlotSelected -= PlayerInventoryUISelected;
            playerInventoryUI.OnSlotSelected -= ShopInventoryUISelected;
            
            ResetSelection();
            
            playerInventoryPanel.SetActive(false);
            shopInventoryPanel.SetActive(false);
            previewPanel.SetActive(false);
            
            _inputManager.EnableMovement();
        }

        #endregion

        #region InventorionSelections

        //Handles selection from player's inventory.
        private void PlayerInventoryUISelected(Clothes clothe)
        {
            audioSource.PlayOneShot(selectSfx);   
            
            _selectedClothe = clothe;
            costLabel.text = _selectedClothe.clotheCost.ToString();

            sellButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);

            clothePreview.gameObject.SetActive(true);
            clothePreview.sprite = _selectedClothe.clotheBodySprite;
        }
        
        //Handles selection from shop's inventory.
        private void ShopInventoryUISelected(Clothes clothe)
        {
            audioSource.PlayOneShot(selectSfx);
            
            _selectedClothe = clothe;
            costLabel.text = _selectedClothe.baseClotheCost.ToString();

            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(false);

            clothePreview.gameObject.SetActive(true);
            clothePreview.sprite = _selectedClothe.clotheBodySprite;
        }

        #endregion

        #region Buy & Sell

        //Buys the selected clothing item.
        private void Buy()
        {
            audioSource.PlayOneShot(buysellSfx);
            if (_selectedClothe.clotheCost > _playerManager.Gold)
            {
                Debug.Log("Not enough gold.");
                return;
            }
            
            _playerManager.ReduceGold(_selectedClothe.baseClotheCost);
            
            //make clothes sell for cheaper after purchase.
            _selectedClothe.clotheCost -= (int)(_selectedClothe.baseClotheCost * 0.20f);
            
            playerInventoryUI.clothesInventory.AddClothes(_selectedClothe);
            shopInventoryUI.clothesInventory.RemoveClothes(_selectedClothe);
            
            ResetSelection();
        }

        //Sells the selected clothing item.
        private void Sell()
        {
            audioSource.PlayOneShot(buysellSfx);
            _playerManager.AddGold(_selectedClothe.clotheCost);
            
            //Unequipped the clothes if player had the sold clothes equipped.
            if (_playerManager.equippedClothes == _selectedClothe)
            {
                _playerManager.UnequipClothes();
            }
            
            shopInventoryUI.clothesInventory.AddClothes(_selectedClothe);
            playerInventoryUI.clothesInventory.RemoveClothes(_selectedClothe);
            
            ResetSelection();
        }

        //Resets selection state to default (off).
        private void ResetSelection()
        {
            indicator.SetActive(false);
            clothePreview.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
            
            costLabel.text = "0";
            _selectedClothe = null;
            clothePreview.sprite = null;
        }
        
        #endregion
    }
}