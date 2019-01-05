using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> (); 
        animator = GetComponent<Animator> ();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        move.y = Input.GetAxis("Vertical");

        

        if(move.x > 0.01f)
        {
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        } 
        else if (move.x < -0.01f)
        {
            if(spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }

        if (move.y > 0.01f)
        {
            if (spriteRenderer.flipX == true)
            {
               // spriteRenderer.flipX = false;
            }
        }
        else if (move.y < -0.01f)
        {
            if (spriteRenderer.flipX == false)
            {
               // spriteRenderer.flipX = true;
            }
        }
        //Debug.Log(velocity.y);
        //animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", Mathf.Abs(velocity.y) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}