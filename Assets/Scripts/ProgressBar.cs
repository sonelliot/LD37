using UnityEngine;
using System;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    private GameObject m_foreground;
    public Func<float> percent;

    public void Start()
    {
        m_foreground = transform.Find("Foreground").gameObject;
    }

    public void Update()
    {
        if (this.percent == null)
            return;

        var xform = m_foreground.transform;
        var scale = xform.localScale;

        xform.localScale = new Vector3(
            Mathf.Clamp(this.percent(), 0f, 1f),
            scale.y,
            scale.z);
    }
}
