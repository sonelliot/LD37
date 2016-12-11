using UnityEngine;
using System.Collections;

public class Brewing : MonoBehaviour
{
    private float m_elapsed;
    public Recipe recipe;
    public float tolerance = 0.25f;

    public bool InProgress
    {
        get { return m_elapsed > 0f; }
    }

    public bool IsBurnt
    {
        get { return Progress > 1f + this.tolerance; }
    }

    public float Progress
    {
        get
        {
            if (recipe == null) return 0f;
            return m_elapsed / recipe.duration;
        }
    }

    public void Begin(Recipe recipe)
    {
        this.recipe = recipe;
        m_elapsed = 0.0001f;
    }

    public void Stop()
    {
        m_elapsed = 0f;
        this.recipe = null;
    }

    public void Update()
    {
        if (this.recipe != null)
        {
            m_elapsed += Time.deltaTime;
        }
    }
}
