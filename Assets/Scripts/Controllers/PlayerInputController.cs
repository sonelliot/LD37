using UnityEngine;
using System.Collections;

namespace WizardWorkshop
{
    /// <summary>
    /// Change movement component based on keyboard input.
    /// </summary>
    public class PlayerInputController : MonoBehaviour
    {
        public MovementComponent movement;

        private void Update()
        {
            var v = Input.GetAxis("Vertical");
            var h = Input.GetAxis("Horizontal");

            movement.direction = new Vector3(h, 0.0f, v).normalized;
        }
    }
}