using UnityEngine;

namespace SOScripts
{
    [CreateAssetMenu(fileName = "New Clothes", menuName = "Clothes/New Clothes", order = 0)]
    public class Clothes : ScriptableObject
    {
        public Sprite clotheIconSprite;
        public Sprite clotheBodySprite;
        public int baseClotheCost;
        public int clotheCost;
        public RuntimeAnimatorController clotheAnimController;
    }
}