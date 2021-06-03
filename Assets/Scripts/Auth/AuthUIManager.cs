//Author:Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthUIManager : MonoBehaviour
{
    #region variables
    public static AuthUIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
           instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    #region buttonFunctions
    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }
    public void RegisterScreen() // Register button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
    #endregion
}
