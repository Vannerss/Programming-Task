using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SOScripts
{
    [CreateAssetMenu(fileName = "New Clothes Inventory", menuName = "Clothes/New Clothes Inventory")]
    public class ClothesInventory : ScriptableObject
    {
        public List<Clothes> clothesInventory = new List<Clothes>();

        public event Action OnInventoryChanged;
        
        /// <summary>
        /// Add clothes to the inventory.
        /// </summary>
        /// <param name="clothe">The clothe to add to inventory.</param>
        public void AddClothes(Clothes clothe)
        {
            clothesInventory.Add(clothe);
            OnInventoryChanged?.Invoke();
        }

        /// <summary>
        /// Remove clothes from inventory. Checks if clothes is in inventory first.
        /// </summary>
        /// <param name="clothe">clothe to remove from inventory.</param>
        public void RemoveClothes(Clothes clothe)
        {
            if (clothesInventory.Contains(clothe))
            {
                clothesInventory.Remove(clothe);
                OnInventoryChanged?.Invoke();
            }
        }
    }
}