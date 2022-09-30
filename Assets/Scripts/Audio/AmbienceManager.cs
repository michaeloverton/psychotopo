using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField] private bool playAmbience;
    [SerializeField] private string ambienceEventName;
    EventInstance ambienceInstance;
    [SerializeField] private bool playMusic;
    [SerializeField] private string musicEventName;
    EventInstance musicInstance;

    // Start is called before the first frame update
    void Start()
    {
        ambienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + ambienceEventName);
        if(playAmbience) ambienceInstance.start();

        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + musicEventName);
        if(playMusic) musicInstance.start();
    }

    public void SetMusicParameter(string parameter, float value) {
        musicInstance.setParameterByName(parameter, value);
    }

}
