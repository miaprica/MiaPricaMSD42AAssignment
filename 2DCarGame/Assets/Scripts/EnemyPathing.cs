using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //a list of type Transform as waypoints are positions in x and y
    [SerializeField] List<Transform> waypointsList;

    [SerializeField] WaveConfig waveConfig;

    //shows the next waypoint
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

        waypointsList = waveConfig.GetWaypoints();
        //set the starting position of the Enemy ship to the 1st waypoint
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

    //takes care of moving Enemy along a path
    private void EnemyMove()
    {
        //   0, 1 , 2     <     3   
        if (waypointIndex <= waypointsList.Count - 1)
        {
            //set the targetPosition to the next waypoint Position
            //targetPosition: where we want to go
            var targetPosition = waypointsList[waypointIndex].transform.position;

            targetPosition.z = 0f;

            //enemyMovement per frame
            var enemyMovement = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            //move from current position, to target position, at enemyMovement speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyMovement);

            //check if we reached targetPosition
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        //if enemy reached last waypoint
        else
        {
            Destroy(gameObject);
        }



    }
}
