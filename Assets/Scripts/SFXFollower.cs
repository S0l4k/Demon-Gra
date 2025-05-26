using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeFollower : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateVolume();
    }

    void Update()
    {
        UpdateVolume(); // jeœli chcesz dynamiczne dostosowanie np. w grach bez prze³adowywania scen
    }

    void UpdateVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        audioSource.volume = volume;
    }
}