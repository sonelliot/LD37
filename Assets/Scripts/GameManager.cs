using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private AudioSource m_cauldronSound;
    public Player player;
    public Cauldron[] cauldrons;

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Start()
    {
        m_cauldronSound = GetComponent<AudioSource>();
    }

    public void Update()
    {
        UpdateSounds();
        UpdateGameState();
    }

    private bool IsBubblingAudible
    {
        get
        {
            foreach (var cauldron in this.cauldrons)
            {
                if (cauldron.IsBrewing)
                {
                    return true;
                }
            }

            return false;
        }
    }

    private void UpdateSounds()
    {
        if (IsBubblingAudible && !m_cauldronSound.isPlaying)
        {
            m_cauldronSound.Play();
        }
        else if (!IsBubblingAudible && m_cauldronSound.isPlaying)
        {
            m_cauldronSound.Stop();
        }
    }

    private void UpdateGameState()
    {
        if (player.currentHealth <= 0)
        {
            Restart();
        }
    }
}
