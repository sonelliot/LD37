using UnityEngine;
using System.Collections;

public class CauldronProgress : MonoBehaviour
{
    private GameObject m_background;
    private GameObject m_foreground;
    private Brewing m_brewing;

    public void Start()
    {
        m_brewing = transform.parent.GetComponent<Brewing>();
        m_foreground = transform.Find("Foreground").gameObject;
        m_background = transform.Find("Background").gameObject;
    }

    public void Update()
    {
        if (!m_brewing.InProgress)
        {
            m_foreground.SetActive(false);
            m_background.SetActive(false);
        }
        else
        {
            m_foreground.SetActive(true);
            m_background.SetActive(true);

            var xform = m_foreground.transform;
            var scale = xform.localScale;

            xform.localScale = new Vector3(
                Mathf.Clamp(m_brewing.Progress, 0f, 1f),
                scale.y,
                scale.z);
        }
    }
}
