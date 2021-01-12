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
    private enum State { idle, run, jump, rest, hurt};
    private State state = State.idle;

    //Inspector Variables
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private float jumpForce = 30;
    [SerializeField]
    private float characterSpeed = 10;
    [SerializeField]
    private int hp = 5;
    [SerializeField]
    private Text playerHPText;
    [SerializeField]
    private float damageForce = 5;


    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public float attackRate = 2f;

    private float nextAttackTime = 0;



    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = hp.ToString();
        if(state != State.hurt)
        {
            CharacterMovement();
        }
        VelocityStateChange();
        anim.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (state != State.hurt)
            {

                state = State.hurt;
                hp--;
                //Interaction on damage
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-damageForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(damageForce, rb.velocity.y);
                }           
            }
            
        }
    }

    private void CharacterMovement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //Movement
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-characterSpeed, rb.velocity.y);
            Vector2 tmp = transform.localScale;
            tmp.x = 1;
            transform.localScale = tmp;
        }
        else if (hDirection > 0)
        {
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


    private void Attack()
    {    
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(damageForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void VelocityStateChange()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if  (Time.time >= nextAttackTime)
            {
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
        else if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("rest");
        }
        else if (!coll.IsTouchingLayers(ground))
        {
            state = State.jump;
        }
        else if(state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 1 && coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
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
