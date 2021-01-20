using UnityEngine;
using System;

[Serializable]
public class Sound{

    public string name;
    public AudioClip clip;

    private AudioSource source;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float volume = 0.7f;

    [Range(0, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0, 0.5f)]
    public float randomPitch = 0.1f;


    public void SetSource(AudioSource source)
    {
        this.source = source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + UnityEngine.Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + UnityEngine.Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.Log("No sound found");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
