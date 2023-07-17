using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { private set; get; }

    public bool shakeEnabled = true;

    public float shakeFadeTime = 2.5f;
    public float rotationMultiplier = 15f;

    private float shakeTimer;
    private float shakePower;
    private float shakeRotation;

    private void Awake()
    {
        Instance = this;
    }

    public void StartShake(float length, float power)
    {
        shakeTimer += length;
        shakePower += power;

        shakeFadeTime = power / length;

        shakeRotation += power * rotationMultiplier;
    }

    private void Update()
    {
        if (!shakeEnabled)
            return;

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }

        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, shakeRotation * Random.Range(-1f, 1f));
    }
}
