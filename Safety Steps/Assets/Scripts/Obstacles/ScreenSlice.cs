using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSlice : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float timer = 2.5f;

    private SpriteRenderer spriteRenderer;

    private Vector2 firstPos;

    private void Start()
    {
        firstPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(MoveIntoPosition());
    }

    IEnumerator MoveIntoPosition()
    {
        bool top = transform.position.y == 10 ? true : false;
        spriteRenderer.flipY = top;
        float newY = top ? 5 : -5;
        Vector3 newPosition = new Vector3(transform.position.x, newY);
        while (timer > 0) 
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        timer = 2.5f;

        while (timer > 0)
        {
            transform.position = Vector3.Lerp(transform.position, firstPos, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        foreach (Collider2D obj in Player.Instance.objectsOver)
        {
            if (obj.gameObject == gameObject)
                Player.Instance.Kill();
        }
    }
}
