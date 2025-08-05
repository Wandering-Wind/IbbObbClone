using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [Space(5)]
    public Rigidbody2D rbPlayer;
    public Transform[] groundCheck;
    public LayerMask groundLayer;
    public LayerMask bluePlayer;
    public LayerMask greenPlayer;
   

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private float boostJump = 20f;
    private int currentLayer ;
    private LayerMask thisMask;

    [Header("Checkpoint system")]
    [Space(5)]
    [SerializeField] public Transform startPt;
    private Transform currentPt;
    public GameObject otherPlayer;

    [Header("Point System")]
    [Space(5)]
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        currentPt = startPt;

        currentLayer = gameObject.layer;
        thisMask = 1<< currentLayer;
    }

    // Update is called once per frame
    void Update()
    {
        rbPlayer.velocity = new Vector2(horizontal * speed, rbPlayer.velocity.y);

        if (!isFacingRight && horizontal > 0)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0)
        {
            Flip();
        }

        AssistedJump();
    }

    public void JumpWASD(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpingPower*rbPlayer.gravityScale);
        }

        if(context.canceled && rbPlayer.velocity.y>0f)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y*0.5f);
        }
    }

    public void JumpArrows(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpingPower*rbPlayer.gravityScale);
        }

        if (context.canceled && rbPlayer.velocity.y > 0f)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        bool bGrounded = false;
        for(int i = 0; i < groundCheck.Length; i++)
        {
            if (Physics2D.OverlapCircle(groundCheck[i].position, 0.2f, groundLayer)){
                bGrounded = true; 
               break;
            }
        }
        return bGrounded;
    }

    void AssistedJump()
    {
        for(int i = 0;i < groundCheck.Length;i++)
        {
            if (Physics2D.OverlapCircle(groundCheck[i].position, 0.2f, bluePlayer) && thisMask == LayerMask.GetMask("GreenPlayer"))
            {
                // bGrounded = true;
                this.rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, boostJump * rbPlayer.gravityScale);
                break;
            }
            else if (thisMask == LayerMask.GetMask("BluePlayer") && Physics2D.OverlapCircle(groundCheck[i].position, 0.2f, greenPlayer))
            {
                this.rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, boostJump * rbPlayer.gravityScale);
                break;
            }
            else
            {
                return;
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void MoveWASD(InputAction.CallbackContext context)
    {
        //function tells us when an action was triggered
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void MoveARROWS(InputAction.CallbackContext context)
    {
        //function tells us when an action was triggered
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            currentPt = collision.gameObject.transform;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
           // print("Collided with enemy");
            this.gameObject.transform.position = currentPt.position;
            otherPlayer.transform.position = currentPt.position;
            if(score >= 5)
            {
                score -= 5;
                print("Current score: " + score);
            }
        }

        if (collision.gameObject.CompareTag("Point"))
        {
            score++;
            print("Current score: "+  score);
            Destroy(collision.gameObject);
        }
    }

  
}
