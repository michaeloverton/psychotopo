using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayTrigger : MonoBehaviour
{
    [SerializeField] private TextManager textManager;
    [SerializeField] private string text;
    private string currentText;
    private int currentTextIndex = 1;
    [SerializeField] private float displaySpeed;
    private float timer = 0;

    void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;
        if(timer > displaySpeed && currentTextIndex != text.Length+1)
        {
            currentText = text.Substring(0, currentTextIndex);
            textManager.ShowText(currentText);
            currentTextIndex++;
            timer = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        textManager.HideText();
        currentText = "";
        currentTextIndex = 0;
        timer = 0;
    }
}
