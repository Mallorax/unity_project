using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Animator anim;

    private Transform player;
    private Transform boss;
    private float distanceToPlayer;
    public bool isFlipped = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        Vector2 tmp = transform.localScale;
        tmp.x = -2;
        boss.localScale = tmp;

    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(player.position, boss.position);
        if(distanceToPlayer <= 20)
        {
            anim.SetBool("isSeeingPlayer", true);
        }
        else
        {
            anim.SetBool("isSeeingPlayer", false);
        }
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
