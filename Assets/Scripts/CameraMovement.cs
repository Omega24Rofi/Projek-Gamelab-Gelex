using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position.z * Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + playerTransform.position.z * Vector3.forward;
    }
}
