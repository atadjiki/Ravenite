using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource Music;
    [SerializeField]
    private AudioSource Ambience;

    [SerializeField]
    private AudioSource FXSource;

    [SerializeField]
    private AudioClip Click;
    [SerializeField]
    private AudioClip Ledger;

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
        FXSource.PlayOneShot(Ledger);
    }

    public void PlayClick()
    {
        FXSource.PlayOneShot(Click);
    }

    public void StartMusic()
    {
        Music.Play();
    }
}
