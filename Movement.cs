using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{


    [SerializeField] public float speed; //lets it be editable from unity
    private Rigidbody2D rigidBody;
    public Animator anim;
    public GameObject contextClue;
    public bool playerInRange;
    private bool onGround;
    public float jumpSpeed = 5;
 


    private void Start()
    {
        onGround = true;
    }

    private void Awake()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        

    }


    private void Update()
    {
        //runs on every frame, great for physics 
        float horizontalInput = Input.GetAxis("Horizontal"); // instead of writing the write side of the code multiple times, i can condense it by making it equal to a float value
        rigidBody.velocity = new Vector3(horizontalInput, rigidBody.velocity.y);

        //flip player from left and right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(3.5f, 3.5f, 1f); // x>1 right x<1 left
            anim.SetBool("moving", true);

          


        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-3.5f, 3.5f, 1f);
            anim.SetBool("moving", true);

        }
        else
        {
            anim.SetBool("moving", false);

        }



        if (Input.GetKeyDown("space") && onGround)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            onGround = false;
            anim.SetBool("jumped", true);
        } else
        {
            anim.SetBool("jumped", false);
            onGround = true;
        }
        




        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, speed);
        }



    }

   

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sign"))
        {
            playerInRange = true;
            contextClue.SetActive(true);

        }

        if (other.gameObject.tag == "Floor" && onGround == false)
        {
            onGround = true;

        }


    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sign"))
        {
            playerInRange = false;
            contextClue.SetActive(false);
        }
    }

}
    
