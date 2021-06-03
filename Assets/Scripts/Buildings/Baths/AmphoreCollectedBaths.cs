//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmphoreCollectedBaths : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the target impacts with the player
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;

            FindObjectOfType<AmphoraManagerBaths>().CheckAmphores();

            //The object dissapears in this time
            Destroy(gameObject, 0.5f);
        }
    }
}
