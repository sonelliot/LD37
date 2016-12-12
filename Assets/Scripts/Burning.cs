using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;

public class Burning : MonoBehaviour
{
    private Brewing m_brewing;
    private SpriteRenderer m_renderer;
    public Color start;
    public Color end;

    public void Start()
    {
        m_brewing = this.transform.parent
            .GetComponent<Brewing>();

        m_renderer = GetComponent<SpriteRenderer>();

        this.transform
            .DOScale(0.6f, 0.6f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void Update()
    {
        if (m_brewing.IsBurning)
        {
            m_renderer.enabled = true;
            m_renderer.color = Color.Lerp(
                this.start, this.end, m_brewing.BurningProgress);
        }
        else
        {
            m_renderer.enabled = false;
        }
    }
}
