using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsCatcher : MonoBehaviour
{
    private HeartsSpawner heartsSpawner;

    private void Start() 
    {
        heartsSpawner = FindObjectOfType<HeartsSpawner>();        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Heart")
        {
            Destroy(other.gameObject);
            heartsSpawner.SpawnHearts(1);
        }    
    }
}
