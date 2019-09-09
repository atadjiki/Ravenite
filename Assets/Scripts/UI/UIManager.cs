﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Singleton vars
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    public GameObject Text_Panel;
    public GameObject Start_Panel;
    public GameObject Prompt_Panel;
    public GameObject Music_Select_Panel;

    public GameObject PreviousButton;
    public GameObject NextButton;
    public GameObject SongLabel;
    public TextMeshProUGUI ToggleText;
    public TextMeshProUGUI SongTitle;


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

        Debug.Log(this.name + " Initialized");
    }

    public void SwitchToStartPanel() 
    {
        AllOff();
        Start_Panel.SetActive(true);
    }

    public void SwitchToTextPanel()
    {
        AllOff();
        Text_Panel.SetActive(true);
    }

    public void SwitchToPromptPanel()
    {
        AllOff();
        Prompt_Panel.SetActive(true);
    }

    public void SwitchToMusicSelectPanel()
    {
        AllOff();
        Music_Select_Panel.SetActive(true);
    }

    public void AllOff()
    {
        Text_Panel.SetActive(false);
        Start_Panel.SetActive(false);
        Prompt_Panel.SetActive(false);
        Music_Select_Panel.SetActive(false);
    }

    private void Update()
    {

        PreviousButton.SetActive(GameState.Instance.IsPreviousAvailable());
        NextButton.SetActive(GameState.Instance.IsNextAvailable());
        SongLabel.SetActive(GameState.Instance.IsMusicPlaying());
        SongTitle.text = GameState.Instance.FetchCurrentSong();

        if (GameState.Instance.IsMusicPlaying())
        {
            ToggleText.text = "Music On";
        }
        else
        {
            ToggleText.text = "Music Off";
        }

        

    }
}
