using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartsSpawnPoint; 

    public void SpawnHearts(int heartsAmount)
    {
        StartCoroutine(SpawnHeartsCoroutine(heartsAmount));
    }

    private IEnumerator SpawnHeartsCoroutine(int heartsAmount)
    {
        for(int i = 0; i < heartsAmount; i++)
        {
            yield return StartCoroutine(SpawnHeartAfterTime());
        }
    }

    private IEnumerator SpawnHeartAfterTime()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject spawnedHeart = Instantiate(heartPrefab, new Vector2(heartsSpawnPoint.position.x + Random.Range(-1f, 1f), heartsSpawnPoint.position.y + Random.Range(-1f, 1f)), Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f)));
        spawnedHeart.transform.SetParent(heartsSpawnPoint);
        AudioManager.Instance.PlaySoundWithRandomPitch("heartDrop", 0.5f, 1.7f);
        ParticlesSpawner.Singleton.SpawnParticles(ParticlesId.HeartSpawn, spawnedHeart.transform.position);
    }
}
