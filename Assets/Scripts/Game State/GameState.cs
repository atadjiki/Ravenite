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

    //Prep Start Menu
    public void StartMenu()
    {
        Started = false;
        CameraRig.Instance.SwitchToStart();
        UIManager.Instance.SwitchToStartPanel();
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
        ConvoWaitTimer();
    }

    //Start Next Conversation
    public void StartNextConversation()
    {
        AudioManager.Instance.PlayClick();
        ConversationManager.Instance.NextConversation();
        UIManager.Instance.SwitchToTextPanel();
        SpawnCharacterModel();
        CameraRig.Instance.LookAt(CharacterModel.transform);
        
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
        }
        else if(ConversationManager.Instance.Mode == Constants.Conversation_Mode.Dialogue
            && GameState.Instance.InConversation)
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

        UIManager.Instance.SwitchToPromptPanel();
        AudioManager.Instance.PlayDoorKnock();
        WaitTimerStarted = false;
    }

}
