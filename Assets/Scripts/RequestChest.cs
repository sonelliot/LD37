using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RequestChest : MonoBehaviour, IContainer
{
    public List<Potion> potions;

    public bool IsFull
    {
        get { return this.potions.Count >= 4; }
    }

    public void Interact(Hand hand)
    {
        if (!IsFull && hand.IsFull && hand.Holding<Potion>())
        {
            this.potions.Add(hand.Give<Potion>());
        }
    }

    public void Update()
    {
        for (var i = 0; i < 4; i++)
        {
            var renderer = transform.GetChild(i)
                .gameObject
                .GetComponent<SpriteRenderer>();

            var potion = this.potions
                .ElementAtOrDefault(i);

            if (potion == Potion.Unknown)
            {
                renderer.enabled = false;
            }
            else
            {
                renderer.enabled = true;
                renderer.color = potion.GetColor();
            }
        }
    }
}
