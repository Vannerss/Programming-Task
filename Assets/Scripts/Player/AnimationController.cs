using System;
using UnityEngine;
using Managers.Inputs;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        [Header("Animators")]
        [SerializeField] private Animator animator;
        [SerializeField] private Animator clotheAnimator;

        private PlayerManager _playerManager;
        private InputManager _input;
        
        //Animator parameter hashes.
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int BlendX = Animator.StringToHash("BlendX");
        private static readonly int BlendY = Animator.StringToHash("BlendY");

        private void Start()
        {
            _input = InputManager.Instance;

            animator = GetComponent<Animator>();
            clotheAnimator = transform.GetChild(0).GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _playerManager = PlayerManager.Instance;
            _playerManager.OnClothesEquipChange += UpdateClothes;
        }

        private void OnDisable()
        {
            _playerManager.OnClothesEquipChange -= UpdateClothes;
        }

        private void Update()
        {
            UpdateAnims(_input.GetMovementDirection());
        }
        
        private void UpdateAnims(Vector2 inputDirection)
        {
            if (inputDirection.magnitude > 0.1f)
            {
                animator.SetBool(Moving, true);
                
                if (clotheAnimator.runtimeAnimatorController != null) 
                    clotheAnimator.SetBool(Moving, true);
            }
            else
            {
                animator.SetBool(Moving, false);
                
                if (clotheAnimator.runtimeAnimatorController != null)
                    clotheAnimator.SetBool(Moving, false);
            }
            
            animator.SetFloat(BlendX, inputDirection.x);
            animator.SetFloat(BlendY, inputDirection.y);

            if (clotheAnimator.runtimeAnimatorController == null) return; //Guard Clause
            
            clotheAnimator.SetFloat(BlendX, inputDirection.x);
            clotheAnimator.SetFloat(BlendY, inputDirection.y);
            
        }
        
        private void UpdateClothes()
        {
            //checks if there are any clothes equipped.
            if (_playerManager.equippedClothes == null)
            {
                clotheAnimator.runtimeAnimatorController = null;
                return;
            }
            
            //set the animator controller of the clothe animator to be the equipped clothes anim controller.
            clotheAnimator.runtimeAnimatorController = _playerManager.equippedClothes.clotheAnimController;
        }
    }
}