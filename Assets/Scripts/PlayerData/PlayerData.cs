//Author:Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData 
{
    #region variables
    public string mail;
    public int highscore;
    public string range;
    public float time;
    #endregion

    #region constructor
    public PlayerData(Player player)
    {
        mail = player.mail;
        highscore = player.highscore;
        range = player.range;
        time = player.time;

        
    }
    #endregion

}
