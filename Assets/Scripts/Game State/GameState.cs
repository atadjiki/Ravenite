﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //Singleton vars
    private static GameState _instance;

    public static GameState Instance { get { return _instance; } }

    public bool Started = false;
    public bool InConversation = false;
    public bool WaitTimerStarted = false;

    [SerializeField]
    private float ConvoWaitMin = 4;
    [SerializeField]
    private float ConvoWaitMax = 10;

    public GameObject CharacterModel;


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

    void Build()
    {
        StartMenu();
    }

   

    //Start Game
    public void StartGame()
    {
        Started = true;
        CameraRig.Instance.SwitchToMain();
        AudioManager.Instance.StartMusic();
        AudioManager.Instance.PlayClick();
        UIManager.Instance.AllOff();
        ConvoWaitTimer();

    }

    //Current Conversation Finished
    public void ConversationFinished()
    {
        Debug.Log("No choices available! Waiting for next conversation!");
        UIManager.Instance.AllOff();
        InConversation = false;
        DespawnCharacterModel();
        AudioManager.Instance.PlayDoorClose();

        if (ConversationManager.Instance.AreConversationsAvailable())
        {
            ConvoWaitTimer();
        }
        
    }

    //Start Next Conversation
    public void StartNextConversation()
    {
        AudioManager.Instance.PlayClick();
        ConversationManager.Instance.NextConversation();
        SpawnCharacterModel();
        CameraRig.Instance.LookAtCharacter();
        AudioManager.Instance.PlayDoorOpen();
        
        InConversation = true;
    }

    public void SpawnCharacterModel()
    {
        CharacterModel = Instantiate<GameObject>(CharacterManager.Instance.GetCharacter(ConversationManager.Instance.CurrentConversation.WithCharacter).CharacterModel);
    }

    public void DespawnCharacterModel()
    {
        Destroy(CharacterModel);
    }

    public void Next()
    {

       

        if (GameState.Instance.InConversation == false)
        {
            GameState.Instance.StartNextConversation();
            UIManager.Instance.SwitchToTextPanel();
        }
        else if(ConversationManager.Instance.Mode == Constants.Conversation_Mode.Dialogue
            && GameState.Instance.InConversation && SubtitleManager.Instance.IsWaiting() == false)
        {
            NextLine();
        }
    }

    public void NextLine()
    {
        if (ConversationManager.Instance.NextLine())
        {
            AudioManager.Instance.PlayClick();
        }
    }

    public void NextChoice(Constants.Choice Choice)
    {
        if (ConversationManager.Instance.NextNode(Choice))
        {
            ReputationManager.Instance.AddChoiceFlag(ConversationManager.Instance.CurrentChoice.Flag);
            AudioManager.Instance.PlayClick();
        }
    }


    //Begin Timer for Next Conversation
    public void ConvoWaitTimer()
    {
        StartCoroutine(WaitTimer());
    }

    private IEnumerator WaitTimer()
    {
        WaitTimerStarted = true;
        float WaitTime = Random.Range(ConvoWaitMin, ConvoWaitMax);

        Debug.Log(WaitTime + " seconds until next conversation is available");

        yield return new WaitForSeconds(WaitTime);

        AudioManager.Instance.PlayDoorKnock();
        WaitTimerStarted = false;
        SetCorrectMainPanel();
    }

    //views

    //Prep Start Menu
    public void StartMenu()
    {
        Started = false;
        CameraRig.Instance.SwitchToStart();
        UIManager.Instance.SwitchToStartPanel();
    }

    public void SetCorrectMainPanel()
    {
        if (CameraRig.Instance.Main.enabled)
        {
            if (GameState.Instance.InConversation)
            {
                UIManager.Instance.SwitchToTextPanel();
            }
            else if (GameState.Instance.WaitTimerStarted)
            {
                UIManager.Instance.AllOff();
            }
            else
            {
                UIManager.Instance.SwitchToPromptPanel();
            }
        }
        
    }

    public void SwitchToMainView()
    {
        CameraRig.Instance.SwitchToMain();

        if (GameState.Instance.InConversation)
        {
            UIManager.Instance.SwitchToTextPanel();
        }
        else if (GameState.Instance.WaitTimerStarted)
        {
            UIManager.Instance.AllOff();
        }
        else
        {
            UIManager.Instance.SwitchToPromptPanel();
        }
    }


    public void TogglePhonoMode()
    {
        if(InConversation == false && CameraRig.Instance.Phono.enabled == false)
        {
            CameraRig.Instance.TogglePhonoCamera();
            AudioManager.Instance.PlayClick();
            UIManager.Instance.SwitchToMusicSelectPanel();
        }
        else 
        {
            SwitchToMainView();
        }
    }

    public void ToggleLedgerMode()
    {
        if(CameraRig.Instance.Ledger.enabled == false)
        {
            CameraRig.Instance.ToggleLedgerCamera();
            AudioManager.Instance.PlayLedgerOpen();
            UIManager.Instance.AllOff();
        }
        else
        {
            SwitchToMainView();
            AudioManager.Instance.PlayLedgerClose();
        }

        
    }

    public void CameraZoomIn()
    {
        CameraRig.Instance.MainCameraZoomIn();
    }

    public void CameraZoomOut()
    {
        CameraRig.Instance.MainCameraZoomOut();
    }

    public void PreviousTrack()
    {
        AudioManager.Instance.PreviousTrack();
        AudioManager.Instance.PlayRecordSound();
    }

    public void NextTrack()
    {
        AudioManager.Instance.NextTrack();
        AudioManager.Instance.PlayRecordSound();
    }

    public void ToggleMusic()
    {
       AudioManager.Instance.ToggleMusic();
    }

    public bool IsMusicPlaying()
    {
       return  AudioManager.Instance.MusicOn;
    }

    public bool IsPreviousAvailable()
    {
        return AudioManager.Instance.IsPreviousAvailable();
    }

    public bool IsNextAvailable()
    {
        return AudioManager.Instance.IsNextAvailable();
    }

    public string FetchCurrentSong()
    {
        return AudioManager.Instance.FetchSongTitle();
    }

    public void DoCigaretteEffect()
    {
        AudioManager.Instance.PlayCigarette();
        GameObject.FindObjectOfType<AshtrayInteraction>().PlayEffect();
    }

    public void DoLamp()
    {
        AudioManager.Instance.PlayLamp();
        GameObject.FindObjectOfType<LampInteraction>().PlayEffect();
    }
}
