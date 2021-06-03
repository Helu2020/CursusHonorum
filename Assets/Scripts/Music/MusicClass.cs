//Author: Héctor Luis De Pablo
using UnityEngine;

public class MusicClass : MonoBehaviour
{
    #region variable
    private AudioSource audioSource;
    #endregion
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
