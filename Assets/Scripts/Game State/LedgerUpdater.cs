using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedgerUpdater : MonoBehaviour
{

    public Image NeighborhoodImage;
    public Image CopsImage;
    public Image RivalsImage;

    public LedgerInfo[] LedgerInfo;

    //Singleton vars
    private static LedgerUpdater _instance;

    public static LedgerUpdater Instance { get { return _instance; } }


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
        SetFill(Constants.Faction.Cops);
        SetFill(Constants.Faction.Neighborhood);
        SetFill(Constants.Faction.Rivals);
        SetFill(Constants.Faction.Rivals);

        foreach(LedgerInfo l in LedgerInfo)
        {
            l.gameObject.SetActive(false);
        }

    }

    public void SetFill(Constants.Faction Faction)
    {
        if (Faction == Constants.Faction.Neighborhood)
        {
            NeighborhoodImage.fillAmount = ReputationManager.Instance.GetNormalizedValue(Constants.Faction.Neighborhood);
        }
        else if (Faction == Constants.Faction.Cops)
        {
            CopsImage.fillAmount = ReputationManager.Instance.GetNormalizedValue(Constants.Faction.Cops);
        }
        else if (Faction == Constants.Faction.Rivals)
        {
            RivalsImage.fillAmount = ReputationManager.Instance.GetNormalizedValue(Constants.Faction.Rivals);
        }
    }

    public void SetConversationNotes()
    {
        
        for(int i = 0; i < ConversationManager.Instance.PreviousConversations.Count; i++)
        {
            LedgerInfo[i].gameObject.SetActive(true);
            LedgerInfo[i].Character.text = ConversationManager.Instance.PreviousConversations[i].WithCharacter.ToString();
            LedgerInfo[i].Flag.text = ConversationManager.Instance.PreviousConversations[i].FinalFaction.ToString();
           
            if(ConversationManager.Instance.PreviousConversations[i].FinalModifier == Constants.Modifier.Increment)
            {
                LedgerInfo[i].Modifier.text = "+";
            }
            else if(ConversationManager.Instance.PreviousConversations[i].FinalModifier == Constants.Modifier.Decrement)
            {
                LedgerInfo[i].Modifier.text = "-";
            }
            else
            {
                LedgerInfo[i].Modifier.text = "";
            }
        }
    }
}
