using UnityEngine;

namespace SOScripts
{
    [CreateAssetMenu(fileName = "New Clothes", menuName = "Clothes/New Clothes", order = 0)]
    public class Clothes : ScriptableObject
    {
        //due to the nature of the assets being used two sprites needed in different resolutions are needed.
        public Sprite clotheIconSprite;
        public Sprite clotheBodySprite;
        
        public int baseClotheCost; //price to be used when buying.
        public int clotheCost;     //price to be used when selling.
        
        public RuntimeAnimatorController clotheAnimController;
    }
}