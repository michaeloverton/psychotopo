using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private List<Light> lights;
    private Dictionary<Light, bool> onOffMap = new Dictionary<Light, bool>();
    [SerializeField] float lightChangeTime = 0.5f;
    private float currentTime = 0f;
    private int currentLightIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Light l in lights)
        {
            onOffMap[l] = false;
            l.enabled = false;
        }

        lights[0].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > lightChangeTime)
        {
            lights[currentLightIndex].enabled = false;
            currentLightIndex = (currentLightIndex + 1) % lights.Count;
            lights[currentLightIndex].enabled = true;
            currentTime = 0f;
        }

        currentTime += Time.deltaTime;
    }
}
