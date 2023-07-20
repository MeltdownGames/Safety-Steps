using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    public List<Gamemode> gamemodes = new List<Gamemode>();
    public static GamemodeType currentGamemode;

    public float spawnTimer = 1.5f;

    public Transform obstacleHolder;
    public Transform[] corners;
    public Transform[] rocketSpawns;
    public List<GameObject> fans;
    
    public GameObject[] obstacles;

    public AudioSource startMessageAudio;

    private List<Obstacle> activeObstacles = new List<Obstacle>();

    private float timer;

    private bool pauseSpawning = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerData.LoadScore();

        GamemodeInitiliazing();

        pauseSpawning = true;

        StartCoroutine(_Start());
        IEnumerator _Start()
        {
            startMessageAudio.Play();
            yield return new WaitUntil(() => startMessageAudio.isPlaying == false);
            pauseSpawning = false;
        }
    }

    void GamemodeInitiliazing()
    {
        foreach (Gamemode gamemode in gamemodes)
            if (gamemode.type == currentGamemode)
            {
                spawnTimer = gamemode.spawnSpeed;
            }
    }

    private void Update()
    {
        if (Player.Instance.dead)
            return;

        timer += Time.deltaTime;

        if (timer > spawnTimer)
        {
            GameObject newObstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstacleHolder);
            newObstacle.name = newObstacle.name.Replace("(Clone)", string.Empty);

            ObstacleType obsType = ObstacleType.None;
            switch (newObstacle.name)
            {
                case "HorizontalLaser":
                    newObstacle.transform.position = new Vector2(0, Random.Range(corners[0].position.y, corners[1].position.y));
                    obsType = ObstacleType.HorizontalLaser;
                    break;
                case "Bomb":
                    newObstacle.transform.position = new Vector2(Random.Range(corners[0].position.x, corners[1].position.x), Random.Range(corners[0].position.y, corners[1].position.y));
                    obsType = ObstacleType.Bomb;
                    break;
                case "ScreenSlice":
                    List<float> possibleYPositions = new List<float>()
                    {
                        5,
                        -5,
                    };

                    float selectedYPos = possibleYPositions[Random.Range(0, possibleYPositions.Count)];
                    if (RuleChecks(ObstacleType.ScreenSlice))
                    {
                        Destroy(newObstacle);
                        return;
                    }

                    newObstacle.transform.position = new Vector2(0, selectedYPos);

                    if (newObstacle.transform.position.y == 5)
                        newObstacle.transform.rotation = Quaternion.Euler(0, 0, 180);
                    else
                        newObstacle.transform.rotation = Quaternion.Euler(0, 0, 0);

                    obsType = ObstacleType.ScreenSlice;
                    break;
                case "Rockets":
                    if (RuleChecks(ObstacleType.ScreenSlice))
                    {
                        Destroy(newObstacle);
                        return;
                    }

                    newObstacle.transform.position = new Vector2();
                    obsType = ObstacleType.Rockets;
                    break;
                case "Fan":
                    Destroy(newObstacle);
                    GameObject randomFan = fans[Random.Range(0, fans.Count)];
                    randomFan.GetComponent<Fan>().Open();
                    break;
                default:
                    Debug.LogError("Obstacle: " + newObstacle.name + " functionality not implemented.");
                    break;
            }

            Obstacle newObs = new Obstacle();
            newObs.assignedObject = newObstacle;
            newObs.type = obsType;

            activeObstacles.Add(newObs);

            timer = 0f;
        }

        foreach (Obstacle obs in activeObstacles.ToList())
            if (obs.assignedObject == null && activeObstacles.Contains(obs))
                activeObstacles.Remove(obs);
    }

    bool RuleChecks(ObstacleType type)
    {
        bool cantSpawn = false;
        switch (type)
        {
            case ObstacleType.ScreenSlice:
                foreach (Obstacle obs in activeObstacles)
                    if (obs.type == ObstacleType.ScreenSlice)
                        cantSpawn = true;
                break;
            case ObstacleType.Rockets:
                foreach (Obstacle obs in activeObstacles)
                    if (obs.type == ObstacleType.Rockets)
                        cantSpawn = true;
                break;
        }

        return cantSpawn;
    }

    [System.Serializable]
    private class Obstacle
    {
        public GameObject assignedObject;
        public ObstacleType type;
    }

    private enum ObstacleType
    {
        None,
        HorizontalLaser,
        Bomb,
        ScreenSlice,
        Rockets,
    }
}