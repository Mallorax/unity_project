using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EagleGFX : MonoBehaviour
{
    // Start is called before the first frame update
    public AIPath aiPath;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector2(-7, 7);
        }else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector2(7, 7);
        }
    }
}
