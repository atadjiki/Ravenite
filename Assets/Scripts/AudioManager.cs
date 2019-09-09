using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource Ambience;
    public AudioSource FXSource;

    public AudioClip Click;
    public AudioClip Ledger;
    public AudioClip DoorKnock;

    //Singleton vars
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Build();
        Debug.Log(this.gameObject.name + " Initialized");
    }

    public void Build()
    {
        
        Music.loop = true;
        Ambience.loop = true;
        FXSource.loop = false;

        Ambience.Play();
    }

    public void PlayLedger()
    {
        FXSource.clip = Ledger;
        FXSource.Play();
    }

    public void PlayClick()
    {
        FXSource.clip = Click;
        FXSource.Play();
    }

    public void PlayDoorKnock()
    {
        FXSource.clip = DoorKnock;
        FXSource.Play();
    }

    public void StartMusic()
    {
        Music.Play();
    }
}
