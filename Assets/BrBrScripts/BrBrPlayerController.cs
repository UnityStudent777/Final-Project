using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrBrPlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool facingRight = true;
    public Text countText;
    public Text winText;
    private int count;
    public AudioSource jumpsound;


    public float speed;
    public float jumpforce;
    public Animator animator;
    bool jump = false;
    bool crouch = false;

    //ground check
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // private float jumpTimeCounter;
    //public float jumpTime;
    //private bool isJumping;

    //audio stuff




    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        SetCountText();

    }





    void Awake()
    {

        jumpsound = GetComponent<AudioSource>();

    }

    private void Update()
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        float moveHorizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        //Vector2 movement = new Vector2(moveHorizontal, 0);

        // rb2d.AddForce(movement * speed);

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);



        //stuff I added to flip my character
        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {


            if (Input.GetKey(KeyCode.UpArrow))
            {
                // rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                rb2d.velocity = Vector2.up * jumpforce;


                // Audio stuff
                jumpsound.Play();

            }
        }
        if (collision.collider.tag == "enemy" && isOnGround)
        {
            gameObject.SetActive(false);
        }
    }




    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("PickUp"))


            other.gameObject.SetActive(false);


        count = count + 2;


        SetCountText();
    }


    void SetCountText()
    {

        countText.text = "Count: " + count.ToString();


        if (count >= 10)

            winText.text = "You win!";
    }








}