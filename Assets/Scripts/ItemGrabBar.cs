using UnityEngine;
using UnityEngine.UI;

public class ItemGrabBar : MonoBehaviour
{
    public Slider itemSlider;
    //public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float time = 100;
    private float lerpSpeed = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if(itemSlider.value != time)
        {
            itemSlider.value = time;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10);
        }

        //if (itemSlider.value != easeHealthSlider.value)
        //{
        //    easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, time, lerpSpeed);
        //}
    }

    void takeDamage(float damage)
    {
        time -= 1;
    }
}
