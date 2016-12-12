using UnityEngine;
using System.Collections;

public static class GameManager
{
    public static void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
