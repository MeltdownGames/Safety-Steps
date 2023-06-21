using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Rockets : MonoBehaviour
{
    public GameObject rocketPrefab;

    public List<GameObject> alerts = new List<GameObject>();

    private List<GameObject> rockets = new List<GameObject>();
    private List<Rigidbody2D> rocketsRB = new List<Rigidbody2D>();

    public float timer = 2.5f;
    private bool rocketsSpawned = false;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
        {
            for (int i = 0; i < alerts.Count; i++)
            {
                if (alerts[i] != null)
                {
                    Vector3 alertPos = ObstacleSpawner.Instance.rocketSpawns[i].transform.position;
                    alerts[i].transform.position = new Vector3(alertPos.x, alertPos.y, 0);
                }
            }
        }

        if (timer <= 0 && !rocketsSpawned)
        {
            foreach (GameObject alert in alerts)
            {
                GameObject rocket = Instantiate(rocketPrefab, transform);
                rocket.transform.position = alert.transform.position;

                Vector3 offset = Player.Instance.transform.position - rocket.transform.position;
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);
                rocket.transform.rotation = rotation;

                rockets.Add(rocket);
                rocketsRB.Add(rocket.GetComponent<Rigidbody2D>());

                Destroy(alert);
            }

            timer = 10f;
            rocketsSpawned = true;
        }
        else if (rocketsSpawned)
        {
            foreach (GameObject rocket in rockets)
            {
                rocket.transform.position += (rocket.transform.up * 6) * Time.deltaTime;

                foreach (Collider2D obj in Player.Instance.objectsOver)
                    if (obj.gameObject == rocket)
                        Player.Instance.Kill(gameObject.name);
            }

            if (timer <= 0)
                Destroy(gameObject);
        }
    }
}
