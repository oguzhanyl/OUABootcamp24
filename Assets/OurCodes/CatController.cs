using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public Transform player; // Takip edilecek oyuncu
    public float followDistance = 5.0f; // Takip mesafesi
    public float followSpeed = 2.0f; // Takip h�z�

    void Update()
    {
        // Takip edilecek hedef pozisyonu hesapla
        Vector3 targetPosition = player.position - player.forward * followDistance;
        targetPosition.y = transform.position.y; // Y�kseklik ayn� kals�n

        // Objenin mevcut pozisyonu ile hedef pozisyonu aras�ndaki mesafeyi kontrol et
        float distance = Vector3.Distance(transform.position, targetPosition);

        // E�er mesafe belirlenen takip mesafesinden b�y�kse, objeyi hedef pozisyona do�ru hareket ettir
        if (distance > followDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }

        // Oyuncuya d�n
        transform.LookAt(player);
    }
}
