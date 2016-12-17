using UnityEngine;
using System.Collections;

namespace WizardWorkshop
{
    /// <summary>
    /// Contains intended movement direction and speed.
    /// </summary>
    public class MovementComponent : MonoBehaviour
    {
        public Vector3 direction = Vector3.zero;
        public float speed = 1.0f;
        public float braking = 1.0f;
        public float stopMagnitude = 0.1f;
    }
}