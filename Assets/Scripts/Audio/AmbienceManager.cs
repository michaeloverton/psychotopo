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
    [SerializeField] private string trafficEventName;
    EventInstance trafficInstance;

    // Start is called before the first frame update
    void Start()
    {
        ambienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + ambienceEventName);
        if(playAmbience) ambienceInstance.start();

        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + musicEventName);
        if(playMusic) musicInstance.start();

        trafficInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + trafficEventName);
        trafficInstance.start();
    }

    public void SetMusicParameter(string parameter, float value) {
        musicInstance.setParameterByName(parameter, value);
    }

    public void SetCrowdParameter(string parameter, float value) {
        ambienceInstance.setParameterByName(parameter, value);
    }

    public void SetTrafficParameter(string parameter, float value) {
        trafficInstance.setParameterByName(parameter, value);
    }
}
