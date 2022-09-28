using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveTime = 2.0f;
    private float timeSinceMove = 0.0f;
    public float moveMin = 5.0f;
    public float moveMax = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceMove > moveTime) {
            transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(moveMin, moveMax));
            timeSinceMove = 0;
        } else {
            timeSinceMove += Time.deltaTime;
        }
        
    }
}
