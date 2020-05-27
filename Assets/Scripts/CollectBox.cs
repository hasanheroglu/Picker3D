using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBox : MonoBehaviour
{
    private int collectedCount;
    
    // Start is called before the first frame update
    void Start()
    {
        collectedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            collectedCount++;
        }
        
        Debug.Log("Collectibles collected: " + collectedCount);
    }
}
