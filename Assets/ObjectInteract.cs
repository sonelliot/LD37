using UnityEngine;
using System.Collections;

public class ObjectInteract : MonoBehaviour {

    public GameObject _icon;

    public GameObject player;
    public float interactRange;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if ((transform.position - player.transform.position).magnitude < interactRange){
            ToggleInteractIcon(true);
        }
        else{
            ToggleInteractIcon(false);
        }
    }
    void ToggleInteractIcon(bool toggle){
        _icon.SetActive(toggle);
    }
}
