using UnityEngine;



public class PlayerAnimations : MonoBehaviour
{
    //A few variables to move/animate this guy
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer render;
    GameManager gameManager;
    
    void Start()
    {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the user have the controls
        if (gameManager.controls)
        {
            //let's move around!
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
           // rigid.MovePosition(new Vector2(transform.position.x + moveX * speed, transform.position.y + moveY * speed));

            //Not the best way to do it but... change the animator
            if (moveX > 0)
            {
                anim.SetBool("side", true);
                anim.SetBool("top", false);
                anim.SetBool("bottom", false);
                render.flipX = false;
                anim.speed = 1;
            }
            else if (moveX < 0)
            {
                anim.SetBool("side", true);
                anim.SetBool("top", false);
                anim.SetBool("bottom", false);
                render.flipX = true;
                anim.speed = 1;
            }
            else if (moveY < 0)
            {
                anim.SetBool("side", false);
                anim.SetBool("top", false);
                anim.SetBool("bottom", true);
                anim.speed = 1;
            }
            else if (moveY > 0)
            {
                anim.SetBool("side", false);
                anim.SetBool("top", true);
                anim.SetBool("bottom", false);
                anim.speed = 1;
            }
            else
            {
                anim.speed = 0;
            }
        }
        else
        {
            anim.speed = 0;
        }
    }

    //the player cant move
    public void CancelControls()
    {
        gameManager.controls = false;
    }

    //give back the controls to player
    public void GiveBackControls()
    {
        gameManager.controls = true;
    }
}
