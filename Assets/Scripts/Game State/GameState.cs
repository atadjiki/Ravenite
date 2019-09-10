using System.Collections;
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
    private static float ConvoWaitMin = 9;
    [SerializeField]
    private static float ConvoWaitMax = 20;

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
        AudioManager.Instance.PlayClick();
        UIManager.Instance.AllOff();
        ConvoWaitTimer();

    }

    //Current Conversation Finished
    public void ConversationFinished()
    {
        Debug.Log("No dialogue or choices available! Waiting for next conversation!");
        UIManager.Instance.AllOff();
        InConversation = false;
        DespawnCharacterModel();
        AudioManager.Instance.PlayDoorClose();
        ConversationManager.Instance.StashLatestConversation();
        LedgerUpdater.Instance.SetConversationNotes();

        if (ConversationManager.Instance.AreConversationsAvailable())
        {
            ConvoWaitTimer();
        }
        
    }

    //Start Next Conversation
    public void StartNextConversation()
    {
        ConversationManager.Instance.NextConversation();
        SpawnCharacterModel();
        CameraRig.Instance.LookAtCharacter();
        AudioManager.Instance.PlayDoorOpen();

        InConversation = true;
    }

    public void StartNextDialogueSet()
    {
        UIManager.Instance.SwitchToTextPanel();
        AudioManager.Instance.PlayClick();
        CameraRig.Instance.LookAtCharacter();
    }

    public void StartNextChoiceTree()
    {
        UIManager.Instance.SwitchToTextPanel();
        AudioManager.Instance.PlayClick();
        CameraRig.Instance.LookAtCharacter();
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

        if (InConversation == false && WaitTimerStarted == false && CameraRig.Instance.Main.enabled)
        {
            StartNextConversation();
            UIManager.Instance.SwitchToTextPanel();
            AudioManager.Instance.PlayClick();
        }
        else if(ConversationManager.Instance.Mode == Constants.Conversation_Mode.Dialogue
            && InConversation && SubtitleManager.Instance.IsWaiting() == false && CameraRig.Instance.Main.enabled)
        {
            ConversationManager.Instance.NextLine();
        }
        else if(SubtitleManager.Instance.IsWaiting())
        {
            Debug.Log("Can't skip, waiting for dialogue!");
        }
    }

    public void NextChoice(Constants.Choice Choice)
    {

        if (SubtitleManager.Instance.IsWaiting())
        {
            Debug.Log("Can't skip, waiting for choice!");
        }
        else if(ConversationManager.Instance.Mode == Constants.Conversation_Mode.Choice
            && InConversation && SubtitleManager.Instance.IsWaiting() == false && CameraRig.Instance.Main.enabled)
        {
            if (ConversationManager.Instance.NextNode(Choice))
            {
                System.Tuple<Constants.Faction, Constants.Modifier> result = ReputationManager.Instance.AddChoiceFlag(ConversationManager.Instance.CurrentChoice.Flag);

                ConversationManager.Instance.CurrentConversation.FinalFaction = result.Item1;
                ConversationManager.Instance.CurrentConversation.FinalModifier = result.Item2;
                ConversationManager.Instance.CurrentConversation.FinalFlag = ConversationManager.Instance.CurrentChoice.Flag;
            }

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

    public void ToggleCredits()
    {
       
        if (InConversation == false && CameraRig.Instance.Credits.enabled == false)
        {
            Debug.Log("Toggle credits");
            CameraRig.Instance.ToggleCreditsCamera();
            AudioManager.Instance.PlayClick();
            UIManager.Instance.SwitchToCreditsPanel();
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
