using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Set the slider’s starting value to current volume
        volumeSlider.value = AudioListener.volume;

        // Hook up the event by code (optional if you do it in the Inspector)
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        Debug.Log("Volume set to: " + value);
    }
}
