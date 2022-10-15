using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class ButtonCollider : MonoBehaviour
{
    public UnityEvent onClick;
    [SerializeField] EventReference clickEvent;
    [SerializeField] EventReference hoverEvent;
    private bool hoverSoundPlayed = false;

    [SerializeField] private Transform button;
    [SerializeField] private float mouseOverScale = 2;
    private Vector3 initialScale;

    void Start()
    {
        if (onClick == null) onClick = new UnityEvent();
        initialScale = button.transform.localScale;
    }

    void OnMouseOver()
    {
        button.transform.localScale = mouseOverScale * initialScale;
        if(!hoverSoundPlayed)
        {
            FMODUnity.RuntimeManager.PlayOneShot(hoverEvent);
            hoverSoundPlayed = true;
        }
    }

    void OnMouseExit()
    {
        button.transform.localScale = initialScale;
        hoverSoundPlayed = false;
    }

    void OnMouseDown()
    {
        onClick.Invoke();
        FMODUnity.RuntimeManager.PlayOneShot(clickEvent);
    }

    void OnDisable()
    {
        button.transform.localScale = initialScale;
        hoverSoundPlayed = false;
    }
}
