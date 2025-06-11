using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager1 : MonoBehaviour
{
    public GameObject pausePanel, settingsPanel;

    public Slider volumeSlider;
    public Toggle muteCharacterToggle;

    private bool isPaused = false;

    void Start()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);

        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        bool isMuted = PlayerPrefs.GetInt("MuteCharacter", 0) == 1;
        muteCharacterToggle.isOn = isMuted;
        muteCharacterToggle.onValueChanged.AddListener(MuteCharacter);
    }

    public void LoadLevel(string levelName) => SceneManager.LoadScene(levelName);

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void ClosePause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void MuteCharacter(bool isMuted)
    {
        PlayerPrefs.SetInt("MuteCharacter", isMuted ? 1 : 0);

        GameObject character = GameObject.FindGameObjectWithTag("Cat");
        if (character != null)
        {
            AudioSource charAudio = character.GetComponent<AudioSource>();
            if (charAudio != null)
                charAudio.mute = isMuted;
        }
    }

    public void SetVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(volume);
        }
    }
}