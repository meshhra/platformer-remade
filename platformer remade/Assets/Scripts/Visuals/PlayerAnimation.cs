
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Visuals
{
    public class PlayerAnimation : MonoBehaviour 
    {
        // Anim times can be gathered from the state itself, but 
        // for the simplicity ...
        [FormerlySerializedAs("_landAnimDuration")] [SerializeField] private float landAnimDuration = 0.250f;
  

        private PlayerController player;
        private Animator anim;
        private SpriteRenderer spriteRenderer;
    
        private bool grounded;
        private float lockedTill;
        private bool jumpTriggered;
        private bool landed;

        private void Start() 
        {
            
            player = GetComponent<PlayerController>();
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            
            player.OnPlayerJump += () => {
                jumpTriggered = true;
            };
     
            player.OnPlayerLand += () => {
             
                landed = true;
            };
        }
        

        private void Update()
        {
            grounded = player.IsGrounded;
            if (player.XInput!= 0) spriteRenderer.flipX = player.XInput < 0;

            var _state = GetState();

            jumpTriggered = false;
            landed = false;
            
            if (_state == currentState) return;
            anim.CrossFade(_state, 0.3f, 0);
            currentState = _state;
        }

        private int GetState() 
        {
            if (Time.time < lockedTill) return currentState;
            
            // Priorities
            if (landed) return LockState(Land, landAnimDuration);
            if (grounded) return Idle;
            if (jumpTriggered) return Jump;
            return player.Jumping ? Jump : Idle;
           
            //return player.CurrentVerticalSpeed > 0 ? Jump : Idle;
            
            int LockState(int s, float t) {
                lockedTill = Time.time + t;
                return s;
            }
        }

        #region Cached Properties

        private int currentState;

        private static readonly int Idle = Animator.StringToHash("Player Idle");
        //private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Jump = Animator.StringToHash("Player Jump Up");
        //private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Land = Animator.StringToHash("Player Land");
        //private static readonly int Attack = Animator.StringToHash("Attack");
        //private static readonly int Crouch = Animator.StringToHash("Crouch");

        #endregion
    }
    
}