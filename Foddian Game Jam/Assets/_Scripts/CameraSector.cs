using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSector : MonoBehaviour
{
    private new Camera camera;

    private bool panning = false;
    private float smoothSpeed = 1f;

    [SerializeField] private float offsetY;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !panning)
        {
            panning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            panning = false;
        }
    }

    private void LateUpdate()
    {
        Debug.Log(panning);
        if (panning)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + offsetY, camera.transform.position.z);
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetPosition, Time.deltaTime * smoothSpeed);

            // Check if the camera is close enough to the target position to stop panning
            if (Vector3.Distance(camera.transform.position, targetPosition) < 0.01f)
            {
                panning = false;
            }
        }
    }
}
