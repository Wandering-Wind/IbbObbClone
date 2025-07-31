using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    public Transform[] groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
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
        for(int i = 0; i <= groundCheck.Length; i++)
        {
            if (Physics2D.OverlapCircle(groundCheck[i].position, 0.2f, groundLayer)){
                bGrounded = true; 
               break;
            }
        }
        return bGrounded;
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
}
