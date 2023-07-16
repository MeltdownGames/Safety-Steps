using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLaser : Obstacle
{
    public float timer = 2.5f;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            StartCoroutine(EnableAndDestroy());
            IEnumerator EnableAndDestroy()
            {
                Active = true;
                yield return new WaitForEndOfFrame();
                Active = false;
                Destroy(gameObject);
            }
        }
    }
}
