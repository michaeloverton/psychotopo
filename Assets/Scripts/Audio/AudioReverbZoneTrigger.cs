using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReverbZoneTrigger : MonoBehaviour
{
    [SerializeField] private AudioReverbManager reverbManager;
    [SerializeField] private bool exitTrigger;

    void OnTriggerEnter(Collider other)
    {
        reverbManager.StartBasicReverb();
    }

    void OnTriggerExit(Collider other)
    {
        if(exitTrigger) reverbManager.StopBasicReverb();
    }
}
