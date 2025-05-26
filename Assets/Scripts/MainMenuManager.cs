using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject levelPanel, settingsPanel;
    public Button level2Button, level3Button;
    public Slider volumeSlider;
    public Toggle muteCharacterToggle;

    void Start()
    {
        levelPanel.SetActive(false);
        settingsPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Level1Complete", 0) == 1)
            level2Button.interactable = true;
        if (PlayerPrefs.GetInt("Level2Complete", 0) == 1)
            level3Button.interactable = true;

        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        bool isMuted = PlayerPrefs.GetInt("MuteCharacter", 0) == 1;
        muteCharacterToggle.isOn = isMuted;
        muteCharacterToggle.onValueChanged.AddListener(MuteCharacter);
    }

    public void OpenLevels() => levelPanel.SetActive(true);
    public void CloseLevels() => levelPanel.SetActive(false);
    public void OpenSettings() => settingsPanel.SetActive(true);
    public void CloseSettings() => settingsPanel.SetActive(false);

    public void LoadLevel(string levelName) => SceneManager.LoadScene(levelName);
    public void ExitGame() => Application.Quit();

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