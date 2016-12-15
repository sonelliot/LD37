using UnityEngine;
using System.Collections;

namespace WizardWorkshop
{
    /// <summary>
    /// Change the sprite based on movement direction.
    /// </summary>
    public class OmniSpriteController : MonoBehaviour
    {
        public OmniSpriteComponent omni;
        public MovementComponent movement;
        public SpriteRenderer renderer;

        private void Update()
        {
            if (movement.direction.sqrMagnitude > 0.0f)
            {
                renderer.sprite = ForDirection(movement.direction);
            }
        }

        /// <summary>
        /// Figure out which sprite to use for the movement direction.
        /// </summary>
        private Sprite ForDirection(Vector3 direction)
        {
            // Character is facing left or right.
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                var side = Mathf.Sign(direction.x);
                return side > 0.0f ? omni.right : omni.left;
            }
            // Character is facing forwards or backwards.
            else
            {
                var side = Mathf.Sign(direction.z);
                return side > 0.0f ? omni.up : omni.down;
            }
        }
    }
}