using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace SOScripts
{
    [CreateAssetMenu(fileName = "New Clothes", menuName = "Clothes/New Clothes", order = 0)]
    public class Clothes : ScriptableObject
    {
        public string clotheName;
        public Sprite clotheIconSprite;
        public Sprite clotheBodySprite;
        public int baseClotheCost;
        public int clotheCost;
        public AnimatorController clotheAnimController;
    }
}