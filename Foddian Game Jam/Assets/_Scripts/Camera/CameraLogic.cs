using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Transform player;
    public static CameraLogic Instance;
    public bool inSector = false;

    public float smoothSpeed = 10f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void LateUpdate()
    {
        if (!inSector)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y + 1f, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * this.smoothSpeed);
        }
    }
}
