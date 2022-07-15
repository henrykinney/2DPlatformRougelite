using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCol;
    public float speed;
    public float jumpSpeed;
    public float climbSpeed;
    public LayerMask GroundLayer;
    public LayerMask LadderLayer;
    public LayerMask InteractableLayer;
    public float JumpTimeMax;
    public float coyoteTimeMax;
    float coyoteTimeCurrent;
    public BoxCollider2D JumpCollider;
    public BoxCollider2D LadderCollider;
    public BoxCollider2D CeilingCollider;
    float jumpTimeCurrent;
    float VerticalInput;

    bool IsClimbing;
    // Start is called before the first frame update
    void Start()
    {
        jumpTimeCurrent = 0f;
        coyoteTimeCurrent = 0f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        IsClimbing = false;
        VerticalInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //MovePlayer();
        JumpPlayer();
        LadderPlayer();
        //Interact();
    }

    public void MovePlayer(float moveDirection)
    {
        if (!IsClimbing) {
            
            float moveHAxis = moveDirection * speed;
            rb.velocity = new Vector2(moveHAxis, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }
    void JumpPlayer()
    {
        bool IsOnCeiling = CeilingCollider.IsTouchingLayers(GroundLayer);
        
        if (IsOnCeiling) {
            jumpTimeCurrent = 0;
        }
        bool IsOnGround = JumpCollider.IsTouchingLayers(GroundLayer);

        if (IsOnGround || IsClimbing) {
            //print(rc.collider.gameObject.name);
            //jumpTimeCurrent = JumpTimeMax;
            coyoteTimeCurrent = coyoteTimeMax;
        }
        coyoteTimeCurrent -= Time.deltaTime;

        if (Input.GetButton("Jump") && jumpTimeCurrent > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        else
        {
            jumpTimeCurrent = 0f;
        }
        jumpTimeCurrent -= Time.deltaTime;
    }
    public void Jump() {
        if (coyoteTimeCurrent > 0) {
            jumpTimeCurrent = JumpTimeMax;
        }
        if (IsClimbing) {
            StopClimbing();
        }
    }
    
    void LadderPlayer()
    {
        if (VerticalInput >= 0.5) {
            bool IsOnLadder = LadderCollider.IsTouchingLayers(LadderLayer);
            if (IsOnLadder) {
                StartClimbing();
                Grid grid = GameObject.Find("Grid").GetComponent<Grid>();
                Vector3Int CellPosition = grid.WorldToCell(transform.position);
                transform.position = new Vector3(grid.GetCellCenterWorld(CellPosition).x, transform.position.y, 0);
            }
        }
        if (IsClimbing) {
            
            rb.velocity = new Vector2(rb.velocity.x, VerticalInput * climbSpeed);
            //rb.gravityScale = 0f;
            bool IsOnLadder = LadderCollider.IsTouchingLayers(LadderLayer);
            if (!IsOnLadder) {
                StopClimbing();
            }
        }
    }

    public void SetVerticalInput(float V) {
        VerticalInput = V;
    }

    void StartClimbing()
    {
        IsClimbing = true;
        rb.gravityScale = 0;
    }
    void StopClimbing()
    {
        IsClimbing = false;
        rb.gravityScale = 3;
    }
    void Interact()
    {
        if (Input.GetButtonDown("Fire1")) {
            bool IsTouching = LadderCollider.IsTouchingLayers(InteractableLayer);
            if (IsTouching) {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}

