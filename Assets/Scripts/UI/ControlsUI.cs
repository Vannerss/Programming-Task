using Managers.Inputs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ControlsUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button exitButton;
        
        private InputManager _inputManager;
        
        void Start()
        {
            _inputManager = InputManager.Instance;
            _inputManager.DisableMovement();
            exitButton.onClick.AddListener(Close);
        }

        private void Close()
        {
            _inputManager.EnableMovement();
            gameObject.SetActive(false);
        }
    }
}
