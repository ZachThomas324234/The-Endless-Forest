using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    public TextMeshProUGUI ammunitionDisplay;

    public float itemsCollected = 0;

    // Update is called once per frame
    void Update()
    {
        ammunitionDisplay.SetText( itemsCollected + " / " + "5");
    }
}
