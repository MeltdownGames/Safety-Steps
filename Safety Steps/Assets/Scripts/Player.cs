using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [HideInInspector] public Collider2D[] objectsOver;

    private bool dragging;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Movement();
        GetBlocksUnder();
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

    void GetBlocksUnder()
    {
        objectsOver = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
    }

    public void Kill()
    {
        print("Player Died");
    }
}
