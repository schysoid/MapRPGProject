using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public Rigidbody2D rbody;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("Iswalking", true);
            anim.SetFloat("Input_x", movement_vector.x);
            anim.SetFloat("Input_y", movement_vector.y);
        }
        else
        {
            anim.SetBool("Iswalking", false);
        }

        rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);
    }
}