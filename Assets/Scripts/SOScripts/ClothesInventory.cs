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
        
        public void AddClothes(Clothes clothe)
        {
            clothesInventory.Add(clothe);
            OnInventoryChanged?.Invoke();
        }

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