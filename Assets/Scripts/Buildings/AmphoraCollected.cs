//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmphoraCollected : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;

            FindObjectOfType<AmphoraManager>().CheckAmphores();
            Destroy(gameObject, 0.5f);
        }
    }
}
