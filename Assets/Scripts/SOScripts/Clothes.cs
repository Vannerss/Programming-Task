using UnityEditor.Animations;
using UnityEngine;

namespace SOScripts
{
    [CreateAssetMenu(fileName = "New Clothes", menuName = "Clothes/New Clothes", order = 0)]
    public class Clothes : ScriptableObject
    {
        public string clotheName;
        public Sprite clotheSprite;
        public int clotheCost;
        public AnimatorController clotheAnimController;
    }
}