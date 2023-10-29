using System;
using UnityEngine;
using Managers.Inputs;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Animator clotheAnimator;

        private PlayerManager _playerManager;
        private InputManager _input;
        private static readonly int BlendX = Animator.StringToHash("BlendX");
        private static readonly int BlendY = Animator.StringToHash("BlendY");

        private void Start()
        {
            _input = InputManager.Instance;
            
            _playerManager = PlayerManager.Instance;
            _playerManager.OnClothesEquipped += UpdateClothes;

            animator = GetComponent<Animator>();
            clotheAnimator = transform.GetChild(0).GetComponent<Animator>();
        }

        private void UpdateClothes()
        {
            //set the animator controller of the clothe animator to be the equipped clothes anim controller.
            clotheAnimator.runtimeAnimatorController = _playerManager.equippedClothes.clotheAnimController;
        }
        
        private void Update()
        {
            var inputDirection = _input.GetMovementDirection();

            animator.SetFloat(BlendX, inputDirection.x);
            animator.SetFloat(BlendY, inputDirection.y);

            if (clotheAnimator.runtimeAnimatorController == null) return; //Guard Clause
            
            clotheAnimator.SetFloat(BlendX, inputDirection.x);
            clotheAnimator.SetFloat(BlendY, inputDirection.y);
        }
    }
}