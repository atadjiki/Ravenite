using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Singleton vars
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    public GameObject Text_Panel;
    public GameObject Start_Panel;
    public GameObject Prompt_Panel;
    public GameObject Music_Select_Panel;

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

    public void ToggleMusicSelectUI()
    {
        if(Music_Select_Panel.activeSelf)
        {
            Music_Select_Panel.SetActive(false);
        }
        else
        {
            Music_Select_Panel.SetActive(true);
        }
    }

    public void AllOff()
    {
        Text_Panel.SetActive(false);
        Start_Panel.SetActive(false);
        Prompt_Panel.SetActive(false);
        Music_Select_Panel.SetActive(false);
    }

}
