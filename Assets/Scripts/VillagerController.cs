using UnityEngine;
using System.Collections;

public class VillagerController : MonoBehaviour
{
    private Request m_request;
    private GameObject m_speech;
    private GameObject m_icon;
    private GameObject m_amount;

    public void Start()
    {
        var ui = transform.GetChild(0);

        m_speech = ui.GetChild(0).gameObject;
        m_icon     = ui.GetChild(1).gameObject;
        m_amount   = ui.GetChild(2).gameObject;
    }

    public void MakeRequest(Request request)
    {
        m_request = request;
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        yield return new WaitForSeconds(2.0f);
        m_speech.SetActive(true);

        yield return new WaitForSeconds(5.0f);
        m_speech.SetActive(false);
        m_icon.SetActive(true);
        m_amount.SetActive(true);

        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);

        yield return null;
    }
}
