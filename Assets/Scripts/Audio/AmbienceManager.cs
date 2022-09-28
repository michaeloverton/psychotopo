using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField] private string ambienceEventName;
    EventInstance ambienceInstance;

    // Start is called before the first frame update
    void Start()
    {
        ambienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + ambienceEventName);
        ambienceInstance.start();       
    }

}
