using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ScreenSlice : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float timer = 2.5f;

    public Transform warning;
    public Transform obstacle;

    private SpriteRenderer spriteRenderer;
    private AudioSource grindSound;

    private Vector2 firstPos;

    private void Start()
    {
        firstPos = obstacle.position;
        spriteRenderer = obstacle.GetComponent<SpriteRenderer>();
        grindSound = obstacle.GetComponent<AudioSource>();
        obstacle.transform.rotation = warning.position.y == 5 ? Quaternion.Euler(0, 0, 180) : Quaternion.Euler(0, 0, 0);

        StartCoroutine(StartWarning());
    }

    IEnumerator StartWarning()
    {
        bool top = warning.position.y == 5 ? true : false;
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
        CameraZoom.Instance.StartZoom(2.5f, 1f);
        grindSound.Play();

        timer = 2.5f;
        bool top = obstacle.position.y == 17 ? true : false;
        float newY = top ? 5 : -5;
        while (timer > 0) 
        {
            obstacle.position = Vector3.Lerp(obstacle.position, new Vector2(0, newY), Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }

        timer = 2.5f;

        grindSound.Play();
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

        Player.Instance.TestKill(obstacle.gameObject);
    }
}
