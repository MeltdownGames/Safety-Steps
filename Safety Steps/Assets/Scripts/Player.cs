using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public List<Transform> hitboxPositions = new List<Transform>();

    private bool dragging;

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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.GetComponent<Obstacle>());
        if (collision.GetComponent<Obstacle>() != null)
        {
            if (!collision.GetComponent<Obstacle>().Active)
                return;

            print(collision.gameObject.name);
            Kill(collision.gameObject.name);
        }
    }
}