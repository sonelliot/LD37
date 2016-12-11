using UnityEngine;
using System;
using System.Collections;

public class IntervalTimer : MonoBehaviour
{
    public Action thunk;
    public float interval;
    public bool running;

    public void StartTimer(Action thunk)
    {
        if (thunk != null)
        {
            this.thunk = thunk;
            this.running = true;

            StartCoroutine(Tick());
        }
    }

    public void StopTimer()
    {
        this.running = false;
    }

    public IEnumerator Tick()
    {
        while (this.running)
        {
            yield return new WaitForSeconds(this.interval);
            this.thunk();
        }
    }
}
