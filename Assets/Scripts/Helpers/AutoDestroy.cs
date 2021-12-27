using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    float timer = 2f;

    // Start is called before the first frame update
    void Start() {
        Invoke("DeleteObject", timer);
    }

    // Update is called once per frame
    void DeleteObject() {
        Destroy(gameObject);
    }
}
