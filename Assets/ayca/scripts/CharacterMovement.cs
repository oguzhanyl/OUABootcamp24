using UnityEngine;
using static UnityEditor.Progress;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public string targetTag = "Dissolvable";
    public float dissolveRadius = 5f;
    public GameObject dropItemPrefab;
    public AudioClip dissolveSound;

    private Vector3 movement;
    private AudioSource audioSource;

    [SerializeField] GameObject userInventory;
    bool isInventoryOpen;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthManager healthManager;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        userInventory.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        currentHealth = 0;
        healthManager.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movement = new Vector3(moveX, 0f, moveZ).normalized;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayDissolveSound();
            DissolveNearbyObjects();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isInventoryOpen == false)
            {
                userInventory.SetActive(true);
                isInventoryOpen = true;
            }
            else if (isInventoryOpen == true)
            {
                userInventory.SetActive(false);
                isInventoryOpen = false;
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            Item item = other.gameObject.GetComponent<Item>();
            if (item != null)
            {
                if (item.increasesHealth)
                {
                    IncreaseHealth(item.healthIncreaseAmount);
                }

                Destroy(other.gameObject);
                ScoreMAnager.scoreCount += 1;
            }
        }
    }

    void IncreaseHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthManager.SetHealth(currentHealth);
    }

    void DissolveNearbyObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, dissolveRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                Dissolve cubeScript = collider.GetComponent<Dissolve>();
                if (cubeScript != null)
                {
                    cubeScript.StartDissolver();
                }
            }
        }
    }

    void DropItem()
    {
        Vector3 dropPosition = transform.position + transform.forward;
        dropPosition.y = Mathf.Max(dropPosition.y, 0.5f);

        RaycastHit hit;
        if (Physics.Raycast(dropPosition, Vector3.down, out hit, Mathf.Infinity))
        {
            dropPosition.y = hit.point.y + 0.5f;
        }

        GameObject droppedItem = Instantiate(dropItemPrefab, dropPosition, Quaternion.identity);
        DroppedItem droppedItemScript = droppedItem.GetComponent<DroppedItem>();
        if (droppedItemScript != null)
        {
            droppedItemScript.Initialize();
        }
    }

    void PlayDissolveSound()
    {
        if (audioSource != null && dissolveSound != null)
        {
            audioSource.PlayOneShot(dissolveSound);
        }
    }
}
