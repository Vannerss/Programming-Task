using System;
using System.Collections.Generic;
using SOScripts;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private List<Clothes> clothesInventory;
        
        public static PlayerManager Instance;
        
        public int gold;
        
        
        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;
        }

        public void AddToClothesInventory(Clothes clothe)
        {
            if (!clothesInventory.Contains(clothe))
            {
                clothesInventory.Add(clothe);
            }
        }
    }
}