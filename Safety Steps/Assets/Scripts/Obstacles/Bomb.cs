using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timer = 2.5f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ScreenShake.Instance.StartShake(.3f, .1f);
            Player.Instance.TestKill(gameObject);
            Destroy(gameObject);
        }
    }
}
