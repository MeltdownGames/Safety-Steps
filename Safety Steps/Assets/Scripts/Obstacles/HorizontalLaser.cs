using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLaser : MonoBehaviour
{
    public GameObject soundEffect;

    public float timer = 2.5f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GameObject sound = Instantiate(soundEffect, transform.position, transform.rotation);
            Destroy(sound, sound.GetComponent<AudioSource>().clip.length);

            ScreenShake.Instance.StartShake(.5f, .05f);
            Player.Instance.TestKill(gameObject);
            Destroy(gameObject);
        }
    }
}
