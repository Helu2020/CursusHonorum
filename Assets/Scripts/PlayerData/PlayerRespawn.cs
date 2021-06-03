//Author:Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    public void PlayerFallWater()
    {
        SceneManager.LoadScene("GeneralScene");
    }
}
