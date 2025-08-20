using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f; // Kecepatan gerak laser, bisa diatur dari Inspector

    // Update dipanggil setiap frame
    void Update()
    {
        // Menggerakkan objek ke arah kiri (sumbu X negatif)
        // Time.deltaTime digunakan agar gerakan konsisten di semua komputer,
        // tidak terpengaruh oleh frame rate.
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
