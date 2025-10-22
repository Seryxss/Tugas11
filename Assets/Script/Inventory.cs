using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasKey = false;
    public GameObject key;

    public void AddKey()
    {
        hasKey = true;
        key.SetActive(true);
        Debug.Log("Kunci diambil");
    }
}
