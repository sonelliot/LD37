using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WizardWorkshop
{
    /// <summary>
    /// Move the body in the direction specified by the component.
    /// </summary>
    public class MovementController : MonoBehaviour
    {
        public MovementComponent movement;
        public Rigidbody body;

        private void Update()
        {
            // Current velocity of the body being controlled.
            var velocity = body.velocity;

            // If the movement component has some direction then we must move
            // it in that direction at it's desired speed.
            if (movement.direction.sqrMagnitude > 0f)
            {
                var direction = movement.direction;
                var magnitude = movement.speed;

                // We have overshot our target maximum speed so compensate by
                // applying a force in the opposite direction.
                if (velocity.magnitude > movement.speed)
                {
                    magnitude = -1f * (movement.speed - body.velocity.magnitude);
                }

                body.AddForce(direction * magnitude);
            }
            // Otherwise, we should apply a braking force in the opposite
            // direction until it comes to a stop.
            else
            {
                // If the body is approximately stationary then zero out the
                // velocity to stop any oscillation on the spot.
                if (Mathf.Abs(body.velocity.magnitude) < movement.stopMagnitude)
                {
                    body.velocity = Vector3.zero;
                }
                // Otherwise, apply braking forces to slow it down.
                else
                {
                    var direction = -body.velocity.normalized;
                    body.AddForce(direction * movement.braking);
                }
            }
        }
    }
}

