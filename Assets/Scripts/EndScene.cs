using System;
using UnityEditor;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public UI uI;

    public void Awake()
    {
        uI = FindAnyObjectByType<UI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (uI.itemsCollected == 5) 
        {
            Debug.Log("hello");
            Application.Quit();
            //EditorApplication.isPlaying = false;
        }
    }
}
