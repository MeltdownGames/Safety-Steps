using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionParticles;

    public float timer = 2.5f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ScreenShake.Instance.StartShake(.3f, .1f);
            Player.Instance.TestKill(gameObject);

            GameObject particles = Instantiate(explosionParticles, transform.position, transform.rotation);
            Destroy(particles, 2.5f);

            Destroy(gameObject);
        }
    }
}
