using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    // public CharacterController controller;
    public float speed = 12f;
    public float additionalGravity = -20f;
    public float jumpHeight = 5f;
    public float fallMultiplier = 2.5f;
    public int maxJumps = 2;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public float batasKananKiri = 2.7f;
    public float rotationSpeed = 5f;


    // private Vector3 velocity;
    private bool isGrounded;
    private int jumpCount = 0;
    private Animator animasi;
    private Rigidbody rb;
    void Awake()
    {
        animasi = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
            // velocity.y = -2f; // Mencegah karakter melayang setelah mendarat
        }

        // Mengambil input pergerakan dari pemain
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = rb.velocity.y;

        Vector3 move = Vector3.right * x + Vector3.forward * z;
        if (move.magnitude > 1)
        {
            move = move.normalized;
        }

        bool isMoving = move.magnitude > 0.01f;

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move * -1);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            // transform.rotation = targetRotation;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < maxJumps))
        {
            y = Mathf.Sqrt(jumpHeight * -2f * additionalGravity);
            jumpCount++;
        }

        // Mengaplikasikan gravitasi ke karakter
        if (y < 0.01f)
        {
            y += additionalGravity * fallMultiplier * Time.deltaTime; // Jatuh lebih cepat
        }
        else
        {
            y += additionalGravity * Time.deltaTime; // Jatuh normal
        }

        rb.velocity = move * speed + Vector3.up * y;
        // Mengatur animasi berjalan berdasarkan status pergerakan
        animasi.SetBool("isRun", isMoving);
        animasi.SetBool("isGrounded", isGrounded);

        // batas posisi
        if (Mathf.Abs(transform.position.x) > batasKananKiri)
        {
            transform.position = new Vector3(Mathf.Sign(transform.position.x) * batasKananKiri, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kita cek apakah objek yang kita sentuh memiliki tag "Laser"
        if (other.CompareTag("Laser"))
        {
            // Jika iya, cetak pesan ke konsol untuk memastikan ini bekerja
            Debug.Log("Player menyentuh laser! GAME OVER!");

            // Panggil fungsi untuk mengakhiri permainan
            GameOver();
        }
    }

    void GameOver()
    {
        // Di sini kita akan menulis logika apa yang terjadi saat game over.
        // Pilihan paling sederhana adalah me-restart level yang sedang berjalan.

        // Mengambil scene yang sedang aktif saat ini
        Scene currentScene = SceneManager.GetActiveScene();

        // Memuat ulang scene tersebut
        SceneManager.LoadScene(currentScene.name);
    }
}
