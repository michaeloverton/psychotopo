using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] PauseManager pauseManager;
    private Canvas pauseScreen;

    void Start()
    {
        pauseScreen = GetComponent<Canvas>();
        pauseManager.OnPausePressed += onPaused;
    }

    void onPaused(bool val)
    {
        if(val)
        {
            pauseScreen.enabled = true;
        }
        else
        {
            pauseScreen.enabled = false;
        }
    }
}
