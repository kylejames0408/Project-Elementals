using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAudioManager : MonoBehaviour
{
    #region Fields
    private GameObject[] _others;
    private bool _first = false;
    #endregion

    /// <summary>
    /// Sets game audio.
    /// </summary>
    private void Start()
    {
        // If the player hasn't played the game before
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            // Save a default volume of 1
            PlayerPrefs.SetFloat("musicVolume", 1);

            // Set the audio listener's volume
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        }
        else // If the player has played the game before
        {
            // Set the audio listener's volume to their previous setting
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        }
    }

    /// <summary>
    /// Sets persistent music and deletes duplicates on object awakening.
    /// INSPIRED BY > SOURCE: http://answers.unity.com/comments/1816357/view.html
    /// </summary>
    private void Awake()
    {
        // Finds all music objects
        _others = GameObject.FindGameObjectsWithTag("Music");

        // If this is the first object in the list
        if (_others.Length == 1)
        {
            // Save it as the first object
            _first = true;
        }

        // If it's not the first
        if (_first == false)
        {
            // Destroy the game object
            Destroy(gameObject);
        }

        // Make the object persistent
        DontDestroyOnLoad(this.gameObject);
    }
}
