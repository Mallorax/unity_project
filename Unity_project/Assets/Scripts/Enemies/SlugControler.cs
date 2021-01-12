using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugControler : MonoBehaviour
{
    private enum State { walk, damaged}
    private Rigidbody2D rb;

    [SerializeField]
    private int damageForce = 5;
    [SerializeField]
    private float moveSpeed = 0.5f;
    public bool isMovingRight = false;

    
           

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }




    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            transform.Translate(1 * Time.deltaTime * moveSpeed, 0, 0);
            transform.localScale = new Vector2(-7, 7);
        }
        else
        {
            transform.Translate(-1 * Time.deltaTime * moveSpeed, 0, 0);
            transform.localScale = new Vector2(7, 7);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TurnRight"))
        {
            isMovingRight = true;
        }else if (collision.gameObject.CompareTag("TurnLeft"))
        {
            isMovingRight = false;
        }

    }
}
