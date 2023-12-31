using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private List<GameObject> objectsOver = new List<GameObject>();

    private bool dragging;

    [HideInInspector] public bool dead { get; private set; }

    private AudioSource clickSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hit = Physics2D.OverlapPointAll(mousePosition);
        foreach (Collider2D col in hit)
            if (col.gameObject == gameObject && Input.GetMouseButtonDown(0))
            {
                dragging = true;
                clickSound.Play();
            }

        if (dragging)
        {
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                clickSound.Play();
            }

            transform.position = mousePosition;
        }

        CheckFan();
    }

    public void Kill(string objectName)
    {
        PlayerData.SaveScore(MainUI.Instance.score);
        DeathScreen.Instance.PlayerDied();
        dead = true;
        gameObject.SetActive(false);
    }

    public void TestKill(GameObject killObject)
    {
        if (objectsOver.Count == 0 || dead)
            return;

        foreach (GameObject objOver in objectsOver.ToList())
            if (objOver.gameObject == killObject)
            {
                print("Killing from: " + killObject.name); // remove later
                Kill(killObject.name);
            }
    }

    void CheckFan()
    {
        if (dragging)
            return;

        Vector3 positionChange = new Vector3();
        foreach (GameObject objOver in objectsOver.ToList())
            if (ObstacleSpawner.Instance.fans.Contains(objOver))
                positionChange += new Vector3(0, objOver.GetComponent<Fan>().up ? -1.0f : 1.0f);

        transform.position += positionChange * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!objectsOver.Contains(collision.gameObject))
            objectsOver.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectsOver.Contains(collision.gameObject))
            objectsOver.Remove(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!objectsOver.Contains(collision.gameObject))
            objectsOver.Add(collision.gameObject);
    }
}