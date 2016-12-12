using UnityEngine;
using System.Collections;

public class Brewing : MonoBehaviour
{
    private float m_elapsed;
    public Recipe recipe;
    public float burnFactor = 0.4f;
    public float burningFactor = 0.15f;
    public Player player;

    public bool InProgress
    {
        get { return m_elapsed > 0f; }
    }

    public bool IsBurnt
    {
        get { return Progress > 1f + this.burnFactor; }
    }

    public bool IsBurning
    {
        get { return Progress > 1f + this.burningFactor; }
    }

    public bool IsDone
    {
        get { return Progress >= 1f; }
    }

    public float BurningProgress
    {
        get
        {
            if (!IsBurning)
                return 0f;

            return (Progress - 1f) / this.burnFactor;
        }
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
            var brewFactor = this.player.currentBrewing;

            if (!IsDone)
            {
                m_elapsed += brewFactor * Time.deltaTime;
            }
            else
            {
                m_elapsed += Time.deltaTime;
            }
        }
    }
}
