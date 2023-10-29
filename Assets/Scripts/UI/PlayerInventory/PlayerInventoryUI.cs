using Player;
using SOScripts;
using UnityEngine;
using UnityEngine.UI;
using Managers.Inputs;

namespace UI.PlayerInventory
{
    public class PlayerInventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private InventoryUI inventory;
        [SerializeField] private GameObject previewPanel;
        [SerializeField] private Image previewClothe;
        [SerializeField] private Button equipButton;
        [SerializeField] private Button quitButton;
        
        
        private InputManager _inputManager;
        private PlayerManager _playerManager;
        private Clothes _selectedClothes;
        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _inputManager = InputManager.Instance;
            _inputManager.OnOpenInventory += OpenInventory;
            equipButton.onClick.AddListener(Equip);
            quitButton.onClick.AddListener(CloseInventory);
        }

        private void OpenInventory(bool wasOpen)
        {
            inventory.OnSlotSelected += ClotheSelection;
            inventoryPanel.SetActive(true);
            previewPanel.SetActive(true);
            previewClothe.gameObject.SetActive(false);
        }

        private void CloseInventory()
        {
            inventory.OnSlotSelected -= ClotheSelection;
            inventoryPanel.SetActive(false);
            previewPanel.SetActive(false);
            previewClothe.gameObject.SetActive(false);
        }

        
        private void ClotheSelection(Clothes clothe)
        {
            _selectedClothes = clothe;
            equipButton.gameObject.SetActive(true);
            UpdatePreview();
        }
        
        private void UpdatePreview()
        {
            previewClothe.gameObject.SetActive(true);
            previewClothe.sprite = _selectedClothes.clotheBodySprite;
        }

        private void Equip()
        {
            _playerManager.EquipClothes(_selectedClothes);
        }
        
    }
}