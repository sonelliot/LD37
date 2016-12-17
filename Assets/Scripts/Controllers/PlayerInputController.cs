using UnityEngine;
using System.Collections;

namespace WizardWorkshop
{
    /// <summary>
    /// Change movement component based on keyboard input.
    /// </summary>
    public class PlayerInputController : MonoBehaviour
    {
        /// <summary>
        /// Reference to the movement component. This controller will change
        /// the direction based on input.
        /// </summary>
        public MovementComponent movement;

        /// <summary>
        /// When this is turned on any non-zero value on an axis will be
        /// snapped to the maximum (e.g. -1 or 1).
        /// </summary>
        /// <remarks>
        /// This is useful for making the player's diagonal movement changes
        /// responsive.
        /// </remarks>
        public bool snapAxisMovement = false;

        private void Update()
        {
            var v = Input.GetAxis("Vertical");
            var h = Input.GetAxis("Horizontal");

            if (snapAxisMovement && Mathf.Abs(v) > 0f)
            {
                v = Mathf.Sign(v);
            }

            if (snapAxisMovement && Mathf.Abs(h) > 0f)
            {
                h = Mathf.Sign(h);
            }

            movement.direction = new Vector3(h, 0.0f, v).normalized;
        }
    }
}