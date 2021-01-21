using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayBossTrack : MonoBehaviour
{
    private AudioSource audio;

    [SerializeField]
    private Image border;
    [SerializeField]
    private Image fill;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        border.enabled = false;
        fill.enabled = false;
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
            border.enabled = true;
            fill.enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
