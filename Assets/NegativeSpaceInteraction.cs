using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeSpaceInteraction : MonoBehaviour
{

    public float range;
    public float terrainDetection;
    RaycastHit surfaceFound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out surfaceFound, range, terrainDetection))
        {
            
        }
    }
}
