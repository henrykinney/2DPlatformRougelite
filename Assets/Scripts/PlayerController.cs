using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovement plrmove;
    void Start()
    {
        plrmove = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        plrmove.MovePlayer(Input.GetAxisRaw("Horizontal"));
        if (Input.GetButtonDown("Jump")) {
            plrmove.Jump();
        }
        plrmove.SetVerticalInput(Input.GetAxisRaw("Vertical"));
    }
}
