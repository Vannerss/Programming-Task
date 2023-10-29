﻿using System;
using System.Collections.Generic;
using SOScripts;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldAmount;
        
        public static PlayerManager Instance;
        
        public Clothes equippedClothes;

        public event Action OnClothesEquipped;
        
        [field: SerializeField]
        public int Gold { get; private set; }
        
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

        public void ReduceGold(int amount)
        {
            Gold -= amount;
            if (Gold < 0)
            {
                Gold = 0;
            }
            
            UpdateGoldUI();
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            UpdateGoldUI();
        }

        private void UpdateGoldUI()
        {
            goldAmount.text = Gold.ToString();
        }

        public void EquipClothes(Clothes clothe)
        {
            equippedClothes = clothe;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = equippedClothes.clotheBodySprite;
            OnClothesEquipped?.Invoke();
        }
    }
}