using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    #region Fields
    [SerializeField] Slider volumeSlider;
    #endregion


    /// <summary>
    /// Sets game audio.
    /// SOURCE: https://youtu.be/yWCHaTwVblk
    /// </summary>
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    /// <summary>
    /// Changes the game's volume and saves the preference.
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    /// <summary>
    /// Loads the game's preferences into the slider.
    /// </summary>
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    /// <summary>
    /// Saves the player's volume preference.
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
