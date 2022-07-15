using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDoor : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Interactable intcomp = gameObject.GetComponent<Interactable>();
        GameObject gamemanager = GameObject.Find("GameManager");
        intcomp.InteractFn.AddListener(gamemanager.GetComponent<GameManager>().TransitionLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
