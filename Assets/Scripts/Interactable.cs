using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D InteractCollider;
    public LayerMask PlayerLayer;
    public UnityEvent InteractFn;
    void Start()
    {
        InteractCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Interact()
    {
        if (Input.GetButtonDown("Fire1")) {
            bool IsTouching = InteractCollider.IsTouchingLayers(PlayerLayer);
            if (IsTouching) {
                InteractFn.Invoke();
            }
        }
    }
    void Update()
    {
        Interact();
    }
}
