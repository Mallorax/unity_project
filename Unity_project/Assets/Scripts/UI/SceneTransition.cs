using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Transitioning... " + gameObject.tag);
        if (gameObject.CompareTag("SceneTransition"))
        {
            levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
