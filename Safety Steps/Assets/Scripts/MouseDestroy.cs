using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDestroy : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<GameObject> clickedObjects = new List<GameObject>();

            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
                if (hit.collider != null)
                    clickedObjects.Add(hit.collider.gameObject);

            foreach (GameObject clickedObject in clickedObjects)
                if (clickedObject.CompareTag("MouseDestructable"))
                    Destroy(clickedObject);
        }
    }
}
