using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestroyer : MonoBehaviour
{
    private ParticleSystem myParticleSystem;

    private void Start() 
    {
        myParticleSystem = GetComponent<ParticleSystem>();    
    }

    private void Update() 
    {
        if(myParticleSystem.IsAlive() == false)
            Destroy(gameObject);     
    }

}
