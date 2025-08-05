using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsCollider : MonoBehaviour
{
    private Camera cam;
    public BoxCollider2D topCollider, bottomCollider, leftCollider, rightCollider;
    public float thickness = 1f; // How thick the boundary walls are

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        Vector3 camPos = cam.transform.position;

        // Update top collider
        topCollider.size = new Vector2(camWidth, thickness);
        topCollider.transform.position = new Vector3(camPos.x, camPos.y + camHeight / 2f + thickness / 2f, 0f);

        // Update bottom collider
        bottomCollider.size = new Vector2(camWidth, thickness);
        bottomCollider.transform.position = new Vector3(camPos.x, camPos.y - camHeight / 2f - thickness / 2f, 0f);

        // Update left collider
        leftCollider.size = new Vector2(thickness, camHeight);
        leftCollider.transform.position = new Vector3(camPos.x - camWidth / 2f - thickness / 2f, camPos.y, 0f);

        // Update right collider
        rightCollider.size = new Vector2(thickness, camHeight);
        rightCollider.transform.position = new Vector3(camPos.x + camWidth / 2f + thickness / 2f, camPos.y, 0f);
    }
}
