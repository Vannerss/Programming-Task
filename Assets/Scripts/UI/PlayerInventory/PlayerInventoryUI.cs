using Player;
using SOScripts;
using UnityEngine;
using UnityEngine.UI;
using Managers.Inputs;
using UI.Inventory;

namespace UI.PlayerInventory
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip selectSfx;
        [SerializeField] private AudioClip equipSfx;
        
        [Header("Player Inventory")]
        [SerializeField] private InventoryUI inventory;
        [SerializeField] private GameObject inventoryPanel;
        
        [Header("Preview Panel")]
        [SerializeField] private GameObject previewPanel;
        [SerializeField] private Image previewClothe;
        
        [Header("Buttons")]
        [SerializeField] private Button quitButton;
        [SerializeField] private Button equipButton;
        
        private InputManager _inputManager;
        private PlayerManager _playerManager;
        private Clothes _selectedClothes;
        
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _inputManager = InputManager.Instance;
            _inputManager.OnOpenInventory += OpenInventory;
            
            //subscribe method to onclick event.
            equipButton.onClick.AddListener(Equip);
            quitButton.onClick.AddListener(CloseInventory);
        }

        private void OpenInventory(bool wasOpen)
        {
            inventory.OnSlotSelected += ClotheSelection;
            
            equipButton.gameObject.SetActive(false);
            inventoryPanel.SetActive(true);
            previewPanel.SetActive(true);
            
            //Checks if player has any clothes equipped, if equipped displayed as default sprite for preview panel.
            if (previewClothe.GetComponent<Image>().sprite != null)
            {
                previewClothe.GetComponent<Image>().sprite = _playerManager.equippedClothes.clotheBodySprite;
                previewClothe.gameObject.SetActive(true);
            }
            else
            {
                previewClothe.gameObject.SetActive(false);
            }
            
            _inputManager.DisableMovement();
        }

        public void CloseInventory()
        {
            inventory.OnSlotSelected -= ClotheSelection;
            
            equipButton.gameObject.SetActive(false);
            inventoryPanel.SetActive(false);
            previewPanel.SetActive(false);
            previewClothe.gameObject.SetActive(false);
            
            inventory.indicator.SetActive(false);
            
            _inputManager.EnableMovement();
        }
        
        private void Equip()
        {
            audioSource.PlayOneShot(equipSfx);
            _playerManager.EquipClothes(_selectedClothes);
            _selectedClothes = null;
            CloseInventory();
        }
        
        private void ClotheSelection(Clothes clothe)
        {
            audioSource.PlayOneShot(selectSfx);
            _selectedClothes = clothe;
            equipButton.gameObject.SetActive(true);
            UpdatePreview();
        }
        
        private void UpdatePreview()
        {
            previewClothe.gameObject.SetActive(true);
            previewClothe.sprite = _selectedClothes.clotheBodySprite;
        }
    }
}