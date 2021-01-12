using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float currentHp;
    [SerializeField]
    private float maxhp = 15;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
