using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public float updateInterval = 0.5f; //How often should the number update

    private float accum;
    private float fps;
    private int frames;
    private float timeleft;

    // Start is called before the first frame update
    private void Start()
    {
        timeleft = updateInterval;
    }

    // Update is called once per frame
    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = accum / frames;
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(150, 0, 150, 50), $"Fps: {fps}");
    }
}