using System;
using UnityEngine;
using TMPro;
using UnityEditor.Search;

public class ItemPickup : MonoBehaviour
{
    public UI uI;

    public GameObject item;

    public bool holdingE = false;

    public TextMeshProUGUI ammunitionDisplay;

    [Range(0, 1.5f)]public float itemCharge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        uI = FindAnyObjectByType<UI>();

        ammunitionDisplay.SetText( uI.itemsCollected + " / " + "5");
    }

    void Update()
    {

        if (holdingE == true)itemCharge += Time.deltaTime;
        else itemCharge -= Time.deltaTime;

        itemCharge = Math.Clamp (itemCharge, 0, 1.5f);
                    //ammunitionDisplay.SetText( itemsCollected + " / " + "5");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            Debug.Log("hello");
            if (Input.GetKey(KeyCode.E))
            {
                holdingE = true;

                if (itemCharge == 1.5)
                {
                    gameObject.SetActive(false);
                    uI.itemsCollected += 1;
                    //itemCharge = 0;
                }
            }
            else
            {
                holdingE = false;
            }
        }
    }
}
