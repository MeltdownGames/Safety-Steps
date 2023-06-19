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
            foreach (Collider2D obj in Player.Instance.objectsOver)
            {
                if (obj.gameObject == gameObject)
                    Player.Instance.Kill();
            }

            Destroy(gameObject);
        }
    }
}
