using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBossTrack : MonoBehaviour
{
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Music on!! " + gameObject.tag);
        if (gameObject.CompareTag("MusicTriger"))
        {            
            audio.Play();
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
