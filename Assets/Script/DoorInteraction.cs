using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public GameObject messageUI; // drag UI Text di sini
    public Transform doorTransform; // drag object pintu (bisa diri sendiri)
    public Vector3 openOffset = new Vector3(0, 3f, 0); // arah & jarak buka
    public float openSpeed = 2f;

    private bool playerInRange = false;
    private Inventory playerInventory;
    private bool isOpen = false;
    private Vector3 closedPosition;
    private Vector3 targetPosition;

    void Start()
    {
        closedPosition = doorTransform.position;
        targetPosition = closedPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        Inventory inv = other.GetComponent<Inventory>();
        if (inv != null)
        {
            playerInventory = inv;
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            playerInRange = false;
            playerInventory = null;
            if (messageUI != null)
                messageUI.SetActive(false);
        }
    }

    void Update()
    {
        // gerakan animasi buka pintu
        doorTransform.position = Vector3.Lerp(doorTransform.position, targetPosition, Time.deltaTime * openSpeed);

        if (playerInRange && playerInventory != null)
        {
            if (!isOpen)
            {
                if (playerInventory.hasKey)
                {
                    ShowMessage("Tekan E untuk membuka pintu");
                    if (Input.GetKeyDown(KeyCode.E))
                        OpenDoor();
                }
                else
                {
                    ShowMessage("Kamu tidak punya kunci");
                }
            }
        }
    }

    void ShowMessage(string text)
    {
        if (messageUI != null)
        {
            messageUI.SetActive(true);
            var tmpText = messageUI.GetComponent<TMP_Text>(); // ubah ke TMP
            if (tmpText != null)
                tmpText.text = text;
        }
        else
        {
            Debug.Log(text);
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        targetPosition = closedPosition + openOffset;
        ShowMessage("Pintu terbuka");
        Debug.Log("Pintu terbuka");
    }
}
