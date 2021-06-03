//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player falls into the water
        if (collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene("GeneralScene");
        }
    }
}
