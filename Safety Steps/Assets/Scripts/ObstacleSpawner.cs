using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform obstacleHolder;
    public Transform[] corners;
    
    public GameObject[] obstacles;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1.5f)
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
                        10,
                        -10,
                    };
                    newObstacle.transform.position = new Vector2(0, possibleYPositions[Random.Range(0, possibleYPositions.Count)]);
                    break;
                default:
                    Debug.LogError("Obstacle: " + newObstacle.name + " functionality not implemented.");
                    break;
            }

            timer = 0f;
        }
    }
}
