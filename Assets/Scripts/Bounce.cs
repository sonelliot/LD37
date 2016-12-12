using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour
{
    public float offset = 0.1f;
    public float speed = 2f;
    public float start = 0f;

    public void Update()
    {
        var posn = this.transform.localPosition;
        var y = Mathf.Sin(this.start + this.speed * Time.time) * this.offset;

        this.transform.localPosition = new Vector3(posn.x, y, posn.z);
    }
}
