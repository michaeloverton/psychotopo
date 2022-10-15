using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    public delegate void PauseEvent(bool paused);
    public event PauseEvent OnPausePressed;
    private bool paused = false;
    public delegate void SensitivityChangeEvent(float val);
    public event SensitivityChangeEvent OnSensitivityChanged;

    // Update is called once per frame
    void Update()
    {
        // Pause screen
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            paused = !paused;
            
            if(OnPausePressed != null) OnPausePressed(paused);
        }
    }

    public void Unpause()
    {
        paused = false;

        if(OnPausePressed != null) OnPausePressed(paused);
    }

    public bool GetPaused()
    {
        return paused;
    }

    public void ChangeSensitivity(float val)
    {
        if(OnSensitivityChanged != null) OnSensitivityChanged(val);
    }
}
