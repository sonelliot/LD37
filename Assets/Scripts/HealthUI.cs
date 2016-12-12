using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUI : MonoBehaviour
{
    public Player player;
    public Image[] images;
    public Sprite full;
    public Sprite half;
    public Sprite empty;

    public void Start()
    {
        this.images = GetComponentsInChildren<Image>();
    }

    public void Update()
    {
        int health = this.player.Health;

        for (int i = 0; i < 3; i++)
        {
            var image = this.images[i];
            if (health >= 2)
            {
                image.sprite = this.full;
            }
            else if (health == 1)
            {
                image.sprite = this.half;
            }
            else
            {
                image.sprite = this.empty;
            }

            health -= 2;
        }
    }
}
