using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom Instance { get; private set; }

    private Camera cam;

    public float zoomSpeed = 2.5f;

    private float zoomDuration;
    private float zoomPower;

    private float startingZoom;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        startingZoom = cam.orthographicSize;
    }

    private void Update()
    {
        if (zoomDuration > 0)
            zoomDuration -= Time.deltaTime;

        if (zoomDuration <= 0)
        {
            zoomPower = startingZoom;
        }

        float newZoom = Mathf.Lerp(cam.orthographicSize, zoomPower, Time.deltaTime * zoomSpeed);
        cam.orthographicSize = newZoom;
    }

    public void StartZoom(float duration, [Tooltip("This should be a positive number, as the function minuses this number to get the new orthographicSize for the camera.")] float _zoomPower)
    {
        zoomDuration += duration;
        zoomPower = cam.orthographicSize - _zoomPower;
    }
}