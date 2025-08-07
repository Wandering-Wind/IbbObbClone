using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraController : MonoBehaviour
{
    //track both player positions
    public Transform playerWASD;
    public Transform playerArrows;
    //public Rigidbody2D rbWASD;
    //public Rigidbody2D rbArrows;

    public float maxFollowDistance = 15f;    // Max distance before stopping camera movement
    public float zoomDistanceThreshold = 8f; // Distance at which zoom starts
    public float minZoom = 5f;               // Closest zoom
    public float maxZoom = 10f;              // Max zoom out
    public float cameraSpeed = 5f;           // Smooth speed

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        float distance = Vector2.Distance(playerWASD.position, playerArrows.position);
        Vector3 midpoint = (playerWASD.position + playerArrows.position) / 2f;
        Vector3 targetPosition = new Vector3(midpoint.x, midpoint.y, transform.position.z);

        if (distance <= maxFollowDistance)
        {
            // Move camera smoothly to midpoint
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);

            // Zoom dynamically based on distance
            float zoomT = Mathf.InverseLerp(zoomDistanceThreshold, maxFollowDistance, distance);
            float targetZoom = Mathf.Lerp(minZoom, maxZoom, zoomT);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, cameraSpeed * Time.deltaTime);
        }
        else
        {
            // Stop camera movement and zoom when too far apart
            // (Could flash a warning or limit player movement here too)
        }
    }

    /*void ClampPlayerToCameraBounds()
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;

        float minX = transform.position.x - camWidth;
        float maxX = transform.position.x + camWidth;
        float minY = transform.position.y - camHeight;
        float maxY = transform.position.y + camHeight;

        // Clamp WASD player
        Vector2 posWASD = rbWASD.position;
        posWASD.x = Mathf.Clamp(posWASD.x, minX, maxX);
        posWASD.y = Mathf.Clamp(posWASD.y, minY, maxY);
        rbWASD.MovePosition(posWASD); // Safe for physics

        // Clamp Arrow player
        Vector2 posArrows = rbArrows.position;
        posArrows.x = Mathf.Clamp(posArrows.x, minX, maxX);
        posArrows.y = Mathf.Clamp(posArrows.y, minY, maxY);
        rbArrows.MovePosition(posArrows);
    }*/
}
