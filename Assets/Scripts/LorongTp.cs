using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorongTp : MonoBehaviour
{
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = ((int)(playerTransform.position.z / (6 * transform.localScale.x)) - 1) * 6 * (int)transform.localScale.x * Vector3.forward;
    }
}
