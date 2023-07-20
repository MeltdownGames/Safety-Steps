using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public bool up = true;
    public float strength = 1.0f;

    private Animator animator;
    private Collider2D collider;

    private float timer = 0;
    private bool open = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        animator.SetBool("open", open);
        collider.enabled = open;

        if (timer <= 0)
            open = false;
    } 

    public void Open()
    {
        open = true;
        timer = 4f;
    }
}
