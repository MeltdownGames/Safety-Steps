using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool dragging;

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
}
