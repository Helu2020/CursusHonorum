//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    #region fields
    [Header("FIELDS")]
    [SerializeField] private TextMeshProUGUI range;
    [SerializeField] private TextMeshProUGUI higscore;
    [SerializeField] private TextMeshProUGUI time;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //we get the data from playerprefs
        range.text = PlayerPrefs.GetString("range");
        higscore.text = PlayerPrefs.GetInt("highscore").ToString();
        time.text = PlayerPrefs.GetFloat("time").ToString();
    }

    
}
