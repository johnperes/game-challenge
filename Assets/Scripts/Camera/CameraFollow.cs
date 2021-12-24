using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector3 offset;

    void Update() {
        // Updates the camera position according the offset
        transform.position = target.transform.position + offset;
    }

    // Sets the target to follow
    public void SetTarget(GameObject newTarget) {
        target = newTarget;
    }
}
