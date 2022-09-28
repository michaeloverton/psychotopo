using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private string stepSurfaceParam;

    public void SetStepSurface(int surfaceIndex) {
        FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.setParameterByName(stepSurfaceParam, surfaceIndex);
        if(result != FMOD.RESULT.OK) Debug.LogError(result);
    }
}
