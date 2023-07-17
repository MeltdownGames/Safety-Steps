using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private List<GameObject> objectsOver = new List<GameObject>();

    private bool dragging;

    private bool dead;

    private void Awake()
    {
        Instance = this;
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
                dragging = true;

        if (dragging)
        {
            if (Input.GetMouseButtonUp(0))
                dragging = false;

            transform.position = mousePosition;
        }
    }

    public void Kill(string objectName)
    {
        PlayerData.SaveScore(MainUI.Instance.score);
        DeathScreen.Instance.PlayerDied();
        dead = true;
    }

    public void TestKill(GameObject killObject)
    {
        if (objectsOver.Count == 0 || dead)
            return;

        foreach (GameObject objOver in objectsOver)
            if (objOver.gameObject == killObject)
            {
                print("Killing from: " + killObject.name); // remove later
                Kill(killObject.name);
            }
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