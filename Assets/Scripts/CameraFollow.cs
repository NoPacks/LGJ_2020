using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    public Vector3 offset = new Vector3(-3f, 0, -10);
    public float dampingTime = 0.3f;
    public Vector3 velocity = Vector3.zero;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);
        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position,
                destination, ref velocity, dampingTime);
        }
        else
        {
            this.transform.position = destination;
        }
    }
}
