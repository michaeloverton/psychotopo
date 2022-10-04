using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onClick;
    [SerializeField] private float mouseOverScale = 2;

    void Start()
    {
        if (onClick == null) onClick = new UnityEvent();
    }

    void OnMouseOver()
    {
        transform.localScale = new Vector3(40,40,40);
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(20,20,20);
    }

    void OnMouseDown()
    {
        onClick.Invoke();
    }
}
