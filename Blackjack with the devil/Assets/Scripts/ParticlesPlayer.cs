using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    public void Play()
    {
        particles.Play();
    }
}
