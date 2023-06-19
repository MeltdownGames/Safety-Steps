using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSlice : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float timer = 2.5f;

    private void Start()
    {
        StartCoroutine(MoveIntoPosition());
    }

    IEnumerator MoveIntoPosition()
    {
        bool top = transform.position.y == 10 ? true : false;
        float newY = top ? 5 : -5;
        Vector3 newPosition = new Vector3(transform.position.x, newY);
        while (transform.position != newPosition) 
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            foreach (Collider2D obj in Player.Instance.objectsOver)
            {
                if (obj.gameObject == gameObject)
                    Player.Instance.Kill();
            }

            Destroy(gameObject);
        }
    }
}
