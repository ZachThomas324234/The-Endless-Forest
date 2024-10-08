using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Pickupable : Interactable
{
    public bool holdingE;
    public bool hovering;
    public bool charging = false;
    [Range(0, 1f)]public float itemCharge, sliderCharge;
    public float fillTime = 0f;
    public float evilFillTime = 0f;

    public UI uI;
    public Slider itemSlider;
    public TextMeshProUGUI ammunitionDisplay;

    public PlayerMovement pm;

    public void Awake()
    {
        uI = FindAnyObjectByType<UI>();
        pm = FindAnyObjectByType<PlayerMovement>();
        //itemSlider = GetComponent<Slider>();
        itemSlider.minValue = 0;
        itemSlider.maxValue = 1;
    }

    public void Update()
    {
        itemCharge = Math.Clamp (itemCharge + (holdingE? Time.deltaTime: -Time.deltaTime), 0, 1f);
        sliderCharge = Math.Clamp (sliderCharge + (holdingE? Time.deltaTime : -Time.deltaTime), 0, 1f);

        if(holdingE && charging) itemSlider.value = itemCharge;

        if (itemCharge == 1) 
        {
            gameObject.SetActive(false);
            uI.itemsCollected += 1;
            ammunitionDisplay.SetText( uI.itemsCollected + " / " + "5");
            itemSlider.gameObject.SetActive(false);
        }
    }

    public override void MouseOver()
    {
        // Runs when the mouse Hovers Over this
        hovering = true;
    }

    public override void MouseExit()
    {
        // Runs when the mouse Exits this
        hovering = false;
        holdingE = false;
        charging = false;
        itemSlider.gameObject.SetActive(false);

    }

    public override void InteractStart()
    {
        // Runs when E is Pressed on the Object
        hovering = false;
        holdingE = true;
        charging = true;
        itemSlider.gameObject.SetActive(true);
        itemSlider.value = 0;

        //itemSlider.value += Time.deltaTime;
    }

    public override void InteractEnd()
    {
        // Runs when E is Released on the Object
        holdingE = false;
        charging = false;
        itemSlider.gameObject.SetActive(false);
    }
}
