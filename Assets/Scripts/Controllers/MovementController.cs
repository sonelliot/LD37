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
            if (movement.direction.sqrMagnitude > 0f)
            {
                body.AddForce(
                    movement.direction * movement.speed * Time.deltaTime);
            }
            else
            {
                body.velocity = body.velocity * movement.braking;
            }
        }
    }
}

