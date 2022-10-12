using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayTrigger : MonoBehaviour
{
    [SerializeField] private TextManager textManager;
    [SerializeField] private string text;

    void OnTriggerStay(Collider other)
    {
        textManager.ShowText(text);
    }

    void OnTriggerExit(Collider other)
    {
        textManager.HideText();
    }
}
