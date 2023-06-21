using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSlice : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float timer = 2.5f;

    public Transform warning;
    public Transform obstacle;

    private SpriteRenderer spriteRenderer;

    private Vector2 firstPos;

    private void Start()
    {
        firstPos = obstacle.position;
        spriteRenderer = obstacle.GetComponent<SpriteRenderer>();
        StartCoroutine(StartWarning());
    }

    IEnumerator StartWarning()
    {
        bool top = warning.position.y == 5 ? true : false;
        spriteRenderer.flipY = top;
        float newY = top ? 5 : -5;
        Vector3 newPosition = new Vector3(warning.position.x, newY);
        while (timer > 0)
        {
            warning.position = Vector3.Lerp(warning.position, newPosition, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        timer = 2.5f;

        yield return new WaitForSeconds(2.5f);

        warning.gameObject.SetActive(false);

        StartCoroutine(MoveIntoPosition());
    }

    IEnumerator MoveIntoPosition()
    {
        bool top = obstacle.position.y == 5 ? true : false;
        spriteRenderer.flipY = top;
        float newY = top ? 5 : -5;
        while (timer > 0) 
        {
            obstacle.localPosition = Vector3.Lerp(obstacle.position, new Vector2(0, 0), Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        timer = 2.5f;

        while (timer > 0)
        {
            obstacle.position = Vector3.Lerp(obstacle.position, firstPos, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        foreach (Collider2D obj in Player.Instance.objectsOver)
        {
            if (obj.gameObject == obstacle.gameObject)
                Player.Instance.Kill(obstacle.gameObject.name);
        }
    }
}
