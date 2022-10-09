using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCollider : MonoBehaviour
{
    public UnityEvent onClick;
    [SerializeField] private Transform button;
    [SerializeField] private float mouseOverScale = 2;

    void Start()
    {
        if (onClick == null) onClick = new UnityEvent();
    }

    void OnMouseOver()
    {
        button.transform.localScale = new Vector3(40,40,40);
    }

    void OnMouseExit()
    {
        button.transform.localScale = new Vector3(20,20,20);
    }

    void OnMouseDown()
    {
        onClick.Invoke();
    }
}
