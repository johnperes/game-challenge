using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed of the camera")]
    float speed = 10f;

    [SerializeField]
    [Tooltip("Camera current target")]
    GameObject target;

    [SerializeField]
    [Tooltip("Distance of camera and target")]
    Vector3 offset;

    void Update() {
        // Updates the camera position according the offset
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position + offset, speed * Time.deltaTime);
    }

    // Sets the target to follow
    public void SetTarget(GameObject newTarget) {
        target = newTarget;
    }
}
