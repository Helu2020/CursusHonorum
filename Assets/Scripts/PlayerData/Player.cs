//Author:Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region variables
    
    public string mail { get; set; }
    public int highscore { get; set; }
    public string range { get; set; }
    public float time { get; set; }
    #endregion
    #region Cons
    public Player(){}
    public Player(string mail, int highscore, string range, float time)
    {
        this.mail = mail;
        this.highscore = highscore;
        this.range = range;
        this.time = time;
    }
    #endregion
}
