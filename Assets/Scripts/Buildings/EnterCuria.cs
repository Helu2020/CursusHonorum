//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCuria : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player enters in the Senado
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("QuizzScene");
        }
    }
}
