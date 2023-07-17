using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    public float spawnTimer = 1.5f;

    public Transform obstacleHolder;
    public Transform[] corners;
    public Transform[] rocketSpawns;
    
    public GameObject[] obstacles;

    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnTimer)
        {
            GameObject newObstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstacleHolder);
            newObstacle.name = newObstacle.name.Replace("(Clone)", string.Empty);

            switch (newObstacle.name)
            {
                case "HorizontalLaser":
                    newObstacle.transform.position = new Vector2(0, Random.Range(corners[0].position.y, corners[1].position.y));
                    break;
                case "Bomb":
                    newObstacle.transform.position = new Vector2(Random.Range(corners[0].position.x, corners[1].position.x), Random.Range(corners[0].position.y, corners[1].position.y));
                    break;
                case "ScreenSlice":
                    List<float> possibleYPositions = new List<float>()
                    {
                        5,
                        -5,
                    };
                    newObstacle.transform.position = new Vector2(0, possibleYPositions[Random.Range(0, possibleYPositions.Count)]);

                    if (newObstacle.transform.position.y == 5)
                        newObstacle.transform.rotation = Quaternion.Euler(0, 0, 180);
                    else
                        newObstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case "Rockets":
                    newObstacle.transform.position = new Vector2();
                    break;
                default:
                    Debug.LogError("Obstacle: " + newObstacle.name + " functionality not implemented.");
                    break;
            }

            timer = 0f;
        }
    }
}
