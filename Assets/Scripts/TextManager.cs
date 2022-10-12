using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject textCanvas;
    TextMeshProUGUI text;

    void Start()
    {
        text = textCanvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowText(string display)
    {
        text.SetText(display);
        textCanvas.SetActive(true);
    }

    public void HideText()
    {
        textCanvas.SetActive(false);
    }

}
