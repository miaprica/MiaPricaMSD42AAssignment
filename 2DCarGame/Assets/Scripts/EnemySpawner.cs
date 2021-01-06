using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigList;
    [SerializeField] bool looping = false;

    int startingWave = 0;

    IEnumerator Start()
    {
        do
        {
            //start corutine that spawns all waves
            yield return StartCoroutine(SpawnAllWaves());
        }

        while (looping); 
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveToSpawn)
    {
        for (int enemyCount = 1; enemyCount <= waveToSpawn.GetNumberOfEnemies(); enemyCount++)
        {
            //spawn the enemyPrefab from waveToSpawn
            //at the position specified  waveToSpawn waypoints
            var newEnemy = Instantiate(
                            waveToSpawn.GetEnemyPrefab(),
                            waveToSpawn.GetWaypoints()[0].transform.position,
                            Quaternion.identity);

            //select the wave and apply the enemy to it
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveToSpawn);

            //wait spawnTime
            yield return new WaitForSeconds(waveToSpawn.GetTimeBetweenSpawns());
        }


    }

    private IEnumerator SpawnAllWaves()
    {
        foreach (WaveConfig currentWave in waveConfigList)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}
