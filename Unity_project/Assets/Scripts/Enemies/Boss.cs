using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;

    private Transform player;
    private Transform boss;
    private float distanceToPlayer;
    public bool isFlipped = true;
    public float attackRange;
    private float nextAttackTime = 0;
    public float attackRate = 1;
    public float movementSpeed = 7f;

    public float speed = 2.5f;
    public LayerMask layerMask;
    [SerializeField]
    private AudioManager audioManager;
    [SerializeField]
    private PlayerControler playerControler;
    public int attackDamage = 1;

    private enum State { idle, run}
    private State state = State.idle;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        rb = GetComponent<Rigidbody2D> ();
        Vector2 tmp = transform.localScale;
        tmp.x = -2;
        boss.localScale = tmp;

        

    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(player.position, boss.position);
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, movementSpeed * Time.fixedDeltaTime);
        Collider2D[] colider = Physics2D.OverlapCircleAll(rb.position, attackRange, layerMask);
        LookAtPlayer();
        //TODO Boss should stop moving when player is in the air
        if (colider.Length == 0 && distanceToPlayer < 20)
        {
            rb.MovePosition(newPos);
            state = State.run;
        }
        else if (distanceToPlayer > 20 | colider.Length > 0 | player.position.y > rb.position.y + 0.5)
        {
            state = State.idle;
        }
        anim.SetInteger("state", (int)state);
        if (colider.Length > 0 && Time.time >= nextAttackTime)
        {
            anim.SetTrigger("attack");           
            nextAttackTime = Time.time + 1f / attackRate;
            Debug.Log("attack");
        }
        
    }

    public void inflictDamage()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(rb.position, attackRange, layerMask);
        if (hitEnemies.Length != 0)
        {
            foreach (Collider2D player in hitEnemies)
            {
                Debug.Log("We hit" + player.name);
                player.GetComponent<PlayerControler>().damagePlayer(attackDamage, boss);
            }
        }
    }

    public void PlaySwingSound()
    {
        audioManager.PlaySound("attack");
    }

    public void PlayHitSound()
    {
        audioManager.PlaySound("hit");
    }

    public void PlayStepSound()
    {
        audioManager.PlaySound("run");
    }




    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            Vector2 tmp = transform.localScale;
            tmp.x = -2;
            boss.localScale = tmp;
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            Vector2 tmp = transform.localScale;
            tmp.x = 2;
            boss.localScale = tmp;
            isFlipped = true;
        }
    }
}
