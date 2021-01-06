using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypointsList;
    [SerializeField] WaveConfig waveConfig;

    //shows the next waypoint
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

        waypointsList = waveConfig.GetWaypoints();
        transform.position = waypointsList[waypointIndex].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    //setting up the WaveConfig
    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        waveConfig = waveConfigToSet;
    }

    private void EnemyMove()
    {
        if (waypointIndex <= waypointsList.Count - 1)
        {
            var targetPosition = waypointsList[waypointIndex].transform.position;

            targetPosition.z = 0f;

            var enemyMovement = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyMovement);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }

        else
        {
            Destroy(gameObject);
        }



    }
}
