using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindManager : MonoBehaviour
{
    [SerializeField] float maxMind = 100f;
    private float currentMind;
    [SerializeField] float smokingMindAmount = 5f;
    [SerializeField] float partyingMindAmount = 2f;
    [SerializeField] GameObject mindMeter;
    private float currentMeterScale;


    // Start is called before the first frame update
    void Start()
    {
        currentMind = maxMind;
        currentMeterScale = 1;
    }

    public void loseMind(float loseAmount)
    {
        currentMind = Mathf.Clamp(currentMind - (loseAmount * Time.deltaTime), 0, maxMind);
        adjustMeter();
    }

    public void gainMind(float gainAmount)
    {
        currentMind = Mathf.Clamp(currentMind + (gainAmount * Time.deltaTime), 0, maxMind);
        adjustMeter();
    }

    public float getSmokingMindAmount()
    {
        return smokingMindAmount;
    }

    public float getPartyingMindAmount()
    {
        return partyingMindAmount;
    }

    private void adjustMeter()
    {
        float newMeterYScale = Utility.Remap(currentMind, 0, maxMind, 0, 1);
        Vector3 currentMeterScale = mindMeter.transform.localScale;
        mindMeter.transform.localScale = new Vector3(currentMeterScale.x, newMeterYScale, currentMeterScale.z);
    }
}
