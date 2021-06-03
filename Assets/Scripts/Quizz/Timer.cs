//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region variable and field
    [Header("FIELD")]
    [SerializeField] private Text timeText;
    public float time = 0.0f;
    #endregion
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = "Time: " + time.ToString("f0");
        PlayerPrefs.SetFloat("time", time);
    }
}
