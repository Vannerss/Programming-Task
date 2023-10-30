using System;
using SOScripts;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        
        [SerializeField] private TextMeshProUGUI goldAmount;
        
        public Clothes equippedClothes;

        [field: SerializeField] //make the private set modifiable on editor for debugging purposes.
        public int Gold { get; private set; }
        
        public event Action OnClothesEquipChange;
        
        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;
        }

        private void Start()
        {
            UpdateGoldUI();
        }

        /// <summary>
        /// Handles gold reduction.
        /// </summary>
        /// <remarks>If gold becomes less than zero, it gets set to zero.</remarks>
        /// <param name="amount">amount to decrease gold by.</param>
        public void ReduceGold(int amount)
        {
            Gold -= amount;
            if (Gold < 0)
            {
                Gold = 0;
            }
            
            UpdateGoldUI();
        }

        /// <summary>
        /// Handles gold increase.
        /// </summary>
        /// <param name="amount">amount to increase gold by.</param>
        public void AddGold(int amount)
        {
            Gold += amount;
            UpdateGoldUI();
        }

        private void UpdateGoldUI()
        {
            goldAmount.text = Gold.ToString();
        }

        /// <summary>
        /// Handles equip clothe.
        /// </summary>
        /// <remarks>Sends out a signal of equipped clothes changed.</remarks>
        /// <param name="clothe">The clothe to equip.</param>
        public void EquipClothes(Clothes clothe)
        {
            equippedClothes = clothe;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = equippedClothes.clotheBodySprite;
            transform.GetChild(0).gameObject.SetActive(true);
            OnClothesEquipChange?.Invoke();
        }

        /// <summary>
        /// Handles remove of currently equipped clothe.
        /// </summary>
        /// <remarks>Sends out a signal of equipped clothes changed.</remarks>
        public void UnequipClothes()
        {
            equippedClothes = null;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            transform.GetChild(0).gameObject.SetActive(false);
            OnClothesEquipChange?.Invoke();
        }
    }
}