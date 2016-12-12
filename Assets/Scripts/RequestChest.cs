using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RequestChest : MonoBehaviour, IContainer
{
    public RequestQueue queue;
    public AudioSource sound;

    public void Interact(Hand hand)
    {
        if (hand.Holding<Potion>())
        {
            var potion = (Potion)hand.thing;
            var request = this.queue.Match(potion);

            if (request != null)
            {
                this.queue.Complete(request);
                this.sound.Play();
                hand.Discard();
            }
        }
    }
}
