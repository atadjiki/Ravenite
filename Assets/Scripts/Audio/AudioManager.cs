using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource Ambience;
    public AudioSource FXSource;

    public AudioClip Click;
    public AudioClip LedgerOpen;
    public AudioClip LedgerClose;
    public AudioClip DoorKnock;

    public AudioClip DoorOpen;
    public AudioClip DoorClose;
    public AudioClip Cigarette;
    public AudioClip RecordPlayer;
    public AudioClip Lamp;

    public AudioClip Typewriter;
    public AudioClip Carriage;

    public Record[] Songs;
    private Record CurrentSong;
    private int SongIndex;
    public bool MusicOn;

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

        Music.loop = false;
        MusicOn = true;
        Ambience.loop = true;
        FXSource.loop = false;

        Ambience.Play();
        PickRandomTrack();
        PlaySelected();
    }

    public void PlayLedgerOpen()
    {
        FXSource.clip = LedgerOpen;
        FXSource.Play();
    }

    public void PlayLedgerClose()
    {
        FXSource.clip = LedgerClose;
        FXSource.Play();
    }

    public void PlayLamp()
    {
        FXSource.clip = Lamp;
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

    public void PlayDoorOpen()
    {
        FXSource.clip = DoorOpen;
        FXSource.Play();
    }

    public void PlayDoorClose()
    {
        FXSource.clip = DoorClose;
        FXSource.Play();
    }

    public void PlayCigarette()
    {
        FXSource.clip = Cigarette;
        FXSource.Play();
    }

    public void PlayRecordSound()
    {
        FXSource.clip = RecordPlayer;
        FXSource.Play();
    }

    public void PlayTypeWriter()
    {
        FXSource.clip = Typewriter;
        FXSource.Play();
    }

    public void PlayCarriage()
    {
        FXSource.clip = Typewriter;
        FXSource.Play();
    }

    public void StartMusic()
    {
        Music.Play();
    }

    public void ToggleMusic()
    {
        if (MusicOn)
        {
            MusicOn = false;
            Music.Pause();
        }
        else
        {
            MusicOn = true;
            Music.UnPause();
        }
    }

    public void PreviousTrack()
    {
        if(SongIndex - 1 < 0){return;}

        SongIndex--;

        PlaySelected();

    }

    public void NextTrack()
    {
        if (SongIndex + 1 > Songs.Length-1){return;}

        SongIndex++;

        PlaySelected();
    }

    public void PickRandomTrack()
    {
        SongIndex = Random.Range(0, Songs.Length - 1);
        
    }

    public void PlaySelected()
    {
        CurrentSong = Songs[SongIndex];
        Music.clip = CurrentSong.Clip;
        Music.Play();
        Debug.Log("Playing: " + CurrentSong.Title);
    }

    public bool IsPreviousAvailable()
    {
        if(SongIndex == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsNextAvailable()
    {
        if(SongIndex == Songs.Length - 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string FetchSongTitle()
    {
        return "'" + CurrentSong.Title + "' - " + CurrentSong.Artist;
    }


    void Update()
    {
        if (!Music.isPlaying && MusicOn)
        {
            PickRandomTrack();
            PlaySelected();
        }
    }
}
