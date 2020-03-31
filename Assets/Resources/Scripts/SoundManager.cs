using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    public AudioSource audioSource;
    private AudioClip scored;
    private AudioClip death;
    private AudioClip flap;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        scored = Resources.Load<AudioClip>("Sounds/scored");
        death = Resources.Load<AudioClip>("Sounds/death");
        flap = Resources.Load<AudioClip>("Sounds/flap");
    }

    
    public void playScored(){
        audioSource.clip = scored;
        audioSource.PlayOneShot(scored, 0.9f);
    }

    public void playDeath(){
        audioSource.clip = death;
        audioSource.PlayOneShot(death, 0.9f);
    }

    public void playFlap(){
        audioSource.clip = flap;
        audioSource.PlayOneShot(flap, 0.5f);
    }
}
