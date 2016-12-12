using UnityEngine;
using System.Collections;

public class RequestProgress : MonoBehaviour
{
    public void Start()
    {
        var request = transform.parent
            .GetComponent<Request>();

        var bar = transform.Find("Bar")
            .GetComponent<ProgressBar>();

        bar.percent = () => 1f - request.Progress;
    }
}
