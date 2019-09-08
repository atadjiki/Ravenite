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
        Text_Panel.SetActive(false);
        Prompt_Panel.SetActive(false);
        Start_Panel.SetActive(true);
    }

    public void SwitchToTextPanel()
    {
        Text_Panel.SetActive(true);
        Start_Panel.SetActive(false);
        Prompt_Panel.SetActive(false);
    }

    public void SwitchToPromptPanel()
    {
        Text_Panel.SetActive(false);
        Start_Panel.SetActive(false);
        Prompt_Panel.SetActive(true);
    }

    public void AllOff()
    {
        Text_Panel.SetActive(false);
        Start_Panel.SetActive(false);
        Prompt_Panel.SetActive(false);
    }

}
