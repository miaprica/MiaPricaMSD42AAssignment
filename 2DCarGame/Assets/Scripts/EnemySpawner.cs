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
            yield return StartCoroutine(SpawnAllWaves());
        }

        while (looping); 
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        /////////ovde je bilo nula i manje
        for (int enemyCount = 1; enemyCount <= waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                            waveConfig.GetEnemyPrefab(),
                            waveConfig.GetWaypoints()[0].transform.position,
                            Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);  //adding enemy to path
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }


    }

    private IEnumerator SpawnAllWaves()
    {
        foreach (WaveConfig currentWave in waveConfigList)                         ////waiting for enemies ton reach the last waypoint and looping
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}
