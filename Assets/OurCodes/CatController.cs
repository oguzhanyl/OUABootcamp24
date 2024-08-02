using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public Transform player; // Takip edilecek oyuncu
    public float followDistance = 5.0f; // Takip mesafesi
    public float followSpeed = 2.0f; // Takip hýzý

    void Update()
    {
        // Takip edilecek hedef pozisyonu hesapla
        Vector3 targetPosition = player.position - player.forward * followDistance;
        targetPosition.y = transform.position.y; // Yükseklik ayný kalsýn

        // Objenin mevcut pozisyonu ile hedef pozisyonu arasýndaki mesafeyi kontrol et
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Eðer mesafe belirlenen takip mesafesinden büyükse, objeyi hedef pozisyona doðru hareket ettir
        if (distance > followDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }

        // Oyuncuya dön
        transform.LookAt(player);
    }
}
