using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSound : MonoBehaviour
{
    private void Start()
    {
        bool isMuted = PlayerPrefs.GetInt("MuteCharacter", 0) == 1;
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.mute = isMuted;
        }
    }
}