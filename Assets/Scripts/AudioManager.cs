using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer audioMixer;

    void Awake()
    {
        Debug.Log("AudioManager: Awake");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
            SetVolume(savedVolume);
        }
        else if (Instance != this)
        {
            Debug.Log("AudioManager: Destroying duplicate");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("AudioManager: OnDestroy");
    }

    public void SetVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("CaveMasterVol", dB);

        PlayerPrefs.SetFloat("Volume", volume);
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("Volume", 1f);
    }
}