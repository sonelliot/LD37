using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public Vector3 original;
    public float factor = 1.1f;
    public float radius = 2f;
    public Player player;

    public void Start()
    {
        this.original = this.transform.localScale;
    }

    public void Update()
    {
        var distance = Vector3.Distance(
            this.transform.position,
            this.player.transform.position);

        var active =
            (player.target == this) &&
            (distance < this.radius);

        this.transform.localScale = this.original *
            (active ? this.factor : 1f);
    }
}
