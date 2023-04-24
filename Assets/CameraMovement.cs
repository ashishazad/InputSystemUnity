using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private new Transform transform;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    public void Move(Vector2 delta)
    {
        Vector3 currentPos = transform.position;
        Vector3 newPos = new Vector3(currentPos.x + delta.x, currentPos.y, currentPos.z + delta.y);
        transform.SetPositionAndRotation(newPos, transform.rotation);
    }
}
