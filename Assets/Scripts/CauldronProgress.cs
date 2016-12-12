using UnityEngine;
using System.Collections;

public class CauldronProgress : MonoBehaviour
{
    private GameObject m_bar;
    private Brewing m_brewing;

    public void Start()
    {
        m_brewing = transform.parent.GetComponent<Brewing>();
        m_bar = transform.Find("Bar").gameObject;

        var progressBar = m_bar.GetComponent<ProgressBar>();
        progressBar.percent = () => m_brewing.Progress;
    }

    public void Update()
    {
        if (!m_brewing.InProgress)
        {
            m_bar.SetActive(false);
        }
        else
        {
            m_bar.SetActive(true);
        }
    }
}
