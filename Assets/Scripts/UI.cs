using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    public TextMeshProUGUI ammunitionDisplay;

    private ItemPickup iP;

    public float itemsCollected = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        iP = FindAnyObjectByType<ItemPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        ammunitionDisplay.SetText( itemsCollected + " / " + "5");
    }
}
