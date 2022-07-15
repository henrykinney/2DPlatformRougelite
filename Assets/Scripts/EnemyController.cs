using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    PlayerMovement plrmove;
    public LayerMask GroundLayer;
    public BoxCollider2D LeftWallCollider;
    public BoxCollider2D RightWallCollider;
    float FacingDirection;
    // Start is called before the first frame update
    void Start()
    {
        plrmove = gameObject.GetComponent<PlayerMovement>();
        FacingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (LeftWallCollider.IsTouchingLayers(GroundLayer)) {
            FacingDirection = 1;
        }
        if (RightWallCollider.IsTouchingLayers(GroundLayer)) {
            FacingDirection = -1;
        }
        plrmove.MovePlayer(FacingDirection);
    }
}
