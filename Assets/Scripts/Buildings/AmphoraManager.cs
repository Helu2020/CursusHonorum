//Author: Héctor Luis De Pablo
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmphoraManager : MonoBehaviour
{
    #region Field
    [Header("FIELD")]
    [SerializeField] private GameObject missText;
    #endregion
    void Update()
    {
        CheckAmphores();
    }

    public void CheckAmphores()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("Todas las ánforas recogidas");
            missText.SetActive(true);
        }
    }
}
