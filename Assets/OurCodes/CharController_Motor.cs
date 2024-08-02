using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace SunTemple
{
    public class CharController_Motor : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 10f;
        public float jumpForce = 7f;
        public float lookSensitivity = 2f;
        public Camera playerCamera;
        private CharacterController characterController;
        private Vector3 moveDirection;
        private float gravity = 20f;
        private Animator animator;

        // Dikey bakış açısını sınırlamak için
        private float cameraPitch = 0f;
        public float maxPitch = 60f;

        // Dissolve için eklenen özellikler
        public string targetTag = "Dissolvable";
        public float dissolveRadius = 5f;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            Move();
            Rotate();
            Jump();
            if (Input.GetKeyDown(KeyCode.E))
            {
                DissolveNearbyObjects();
            }
        }

        void Move()
        {
            if (characterController.isGrounded)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;
                bool isRunning = Input.GetKey(KeyCode.LeftShift);

                float currentSpeed = isRunning ? runSpeed : walkSpeed;
                moveDirection = move * currentSpeed;

                // Animasyonları tetikle
                if (move != Vector3.zero)
                {
                    if (isRunning)
                    {
                        animator.SetFloat("Speed", 1.0f); // Koşma animasyonu
                    }
                    else
                    {
                        animator.SetFloat("Speed", 0.5f); // Yürüme animasyonu
                    }
                }
                else
                {
                    animator.SetFloat("Speed", 0.0f); // Durma animasyonu
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }

        void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            // Karakterin yatay dönüşünü kontrol et
            transform.Rotate(Vector3.up * mouseX);

            // Kameranın dikey dönüşünü kontrol et
            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -maxPitch, maxPitch);
            playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
        }

        void Jump()
        {
            if (characterController.isGrounded && Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                animator.SetTrigger("Jump"); // Zıplama animasyonunu tetikle
            }
        }

        void DissolveNearbyObjects()
        {
            // Belirli bir yarıçap içindeki tüm colliderları bulur
            Collider[] colliders = Physics.OverlapSphere(transform.position, dissolveRadius);

            // Bulunan her collider üzerinde işlem yapar
            foreach (Collider collider in colliders)
            {
                // Collider'ın targetTag etiketi ile etiketlenip etiketlenmediğini kontrol eder
                if (collider.CompareTag(targetTag))
                {
                    // Dissolve scriptini alır
                    Dissolve cubeScript = collider.GetComponent<Dissolve>();

                    // Eğer Dissolve scripti mevcutsa, StartDissolver fonksiyonunu çağırır
                    if (cubeScript != null)
                    {
                        cubeScript.StartDissolver();
                    }
                }
            }
        }
    }
}


