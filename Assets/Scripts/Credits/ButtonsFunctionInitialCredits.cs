//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctionInitialCredits : MonoBehaviour
{
    // Start is called before the first frame update
    public void Next()
    {
        SceneManager.LoadScene("GeneralScene");
    }

    public void Back()
    {
        SceneManager.LoadScene("Login-Register");
    }
}
