using System;
using UnityEngine;

public class Pickupable : Interactable
{
    public bool holdingE;
    public bool hovering;
    [Range(0, 1f)]public float itemCharge;

    public void Update()
    {
        itemCharge = Math.Clamp (itemCharge + (holdingE? Time.deltaTime : -Time.deltaTime), 0, 1f);
        if (itemCharge == 1) gameObject.SetActive(false);
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
    }

    public override void InteractStart()
    {
        // Runs when E is Pressed on the Object
        hovering = false;
        holdingE = true;
    }

    public override void InteractEnd()
    {
        // Runs when E is Released on the Object
        holdingE = false;
    }
}
