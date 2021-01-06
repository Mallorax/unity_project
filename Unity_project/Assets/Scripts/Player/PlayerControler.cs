using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private Collider2D coll;

    //States
    private enum State { idle, run, jump, rest };
    private State state = State.idle;

    //Inspector Variables
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float characterSpeed;
    [SerializeField]
    private int hp;
    [SerializeField]
    private Text playerHPText;

    private int direction;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = hp.ToString();
        CharacterMovement();
        VelocityStateChange();
        anim.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (state == State.jump)
            {
                Destroy(collision.gameObject);
            }
            hp--;
        }
    }

    private void CharacterMovement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //Movement
        if (hDirection < 0)
        {
            direction = -1;
            rb.velocity = new Vector2(-characterSpeed, rb.velocity.y);
            Vector2 tmp = transform.localScale;
            tmp.x = 1;
            transform.localScale = tmp;
        }
        else if (hDirection > 0)
        {
            direction = 1;
            rb.velocity = new Vector2(characterSpeed, rb.velocity.y);
            Vector2 tmp = transform.localScale;
            tmp.x = -1;
            transform.localScale = tmp;
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void VelocityStateChange()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("rest");
        }
        else if (!coll.IsTouchingLayers(ground))
        {
            state = State.jump;
        }
        else if (Mathf.Abs(rb.velocity.x) > 1 && coll.IsTouchingLayers(ground))
        {
            state = State.run;
        }
        else if (Mathf.Abs(rb.velocity.x) < 1 && coll.IsTouchingLayers(ground))
        {
            state = State.idle;
        }

    }

}
