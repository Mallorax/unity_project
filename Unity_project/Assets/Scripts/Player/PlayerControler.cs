using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private enum State {idle, run, jump, rest};
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //Movement
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
            Vector2 tmp = transform.localScale;
            tmp.x = 1;
            transform.localScale = tmp;
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
            Vector2 tmp = transform.localScale;
            tmp.x = -1;
            transform.localScale = tmp;
        }
        else
        {
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 30);
            state = State.jump;
        }
        VelocityStateChange();
        anim.SetInteger("state", (int)state);
    }

    private void VelocityStateChange()
    {
        if(state == State.jump)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }else if(Mathf.Abs(rb.velocity.x) > 1)
        {
            //running
            state = State.run;

        }else{
            state = State.idle;
        }
    }

}
