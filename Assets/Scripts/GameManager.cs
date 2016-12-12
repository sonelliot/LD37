using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Player player;

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Update()
    {
        if (player.currentHealth <= 0)
        {
            Restart();
        }
    }
}
