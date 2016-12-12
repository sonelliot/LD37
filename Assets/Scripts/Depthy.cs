using UnityEngine;
using System.Collections;

public class Depthy : MonoBehaviour
{
    public bool dynamic = false;

    public void Start()
    {
        ApplyDepth();
    }

    public void Update()
    {
        if (dynamic)
        {
            ApplyDepth();
        }
    }

    private void ApplyDepth()
    {
        this.transform.position = CalculateDepth(
            this.transform.position);
    }

    private Vector3 CalculateDepth(Vector3 posn)
    {
        var z = -1f * (1f - ((posn.y + 3f) / 6f));
        return new Vector3(posn.x, posn.y, z);
    }
}
