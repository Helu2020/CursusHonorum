//Author:Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmphoraManagerBaths : MonoBehaviour
{
    #region Fields
    [Header("FIELDS")]
    [SerializeField] private GameObject missText;
    [SerializeField] private GameObject missText2;
    [SerializeField] private GameObject missText3;
    [SerializeField] private GameObject missText4;
    #endregion
    void Update()
    {
        CheckAmphores();
    }

    public void CheckAmphores()
    {
        if (transform.childCount == 4)
        {
            missText.SetActive(true);
        } 
        else if (transform.childCount == 3)
        {
            missText2.SetActive(true);
        } 
        else if(transform.childCount == 2)
        {
            missText3.SetActive(true);
        }
        else if (transform.childCount == 0)
        {
            missText4.SetActive(true);
        }
    }
}
