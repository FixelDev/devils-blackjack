using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticlesId
{
    HeartSpawn,
    BlackjackTextSpawn
}

public class ParticlesSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Particles
    {
        public ParticlesId particlesId;
        public GameObject particlesPrefab;
    }

    [SerializeField] private List<Particles> particlesList = new List<Particles>();
    public static ParticlesSpawner Singleton;

    private void Awake() 
    {
        Singleton = this;    
    }

    public void SpawnParticles(ParticlesId id, Vector2 position)
    {
        GameObject particlesPrefab = FindParticlesPrefab(id);
        Instantiate(particlesPrefab, position, Quaternion.identity);
    }

    private GameObject FindParticlesPrefab(ParticlesId id)
    {
        Particles particles = particlesList.Find(p => p.particlesId == id);
        
        return particles.particlesPrefab;
    }
}
