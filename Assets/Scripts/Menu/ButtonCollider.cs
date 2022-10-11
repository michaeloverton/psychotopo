using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCollider : MonoBehaviour
{
    public UnityEvent onClick;
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
    }

    void OnMouseExit()
    {
        button.transform.localScale = initialScale;
    }

    void OnMouseDown()
    {
        onClick.Invoke();
    }
}
