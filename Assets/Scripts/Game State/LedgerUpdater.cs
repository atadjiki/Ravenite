using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedgerUpdater : MonoBehaviour
{

    public Image NeighborhoodImage;
    public Image CopsImage;
    public Image RivalsImage;

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
}
