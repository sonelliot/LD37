using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RequestChest : MonoBehaviour, IContainer
{
    public List<Potion> potions;
    public RequestQueue queue;

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
        UpdatePotions();
        UpdateRequests();
    }

    private void UpdateRequests()
    {
        // Find matching requests.
        var matches = this.potions
            .Select(p => new { potion = p, request = this.queue.Match(p)})
            .Where(m => m.request != null)
            .ToList();

        foreach (var match in matches)
        {
            this.queue.Complete(match.request);
            this.potions.Remove(match.potion);
        }
    }

    private void UpdatePotions()
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
