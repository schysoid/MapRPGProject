﻿using UnityEngine;
using UnityEngine.UI;

public class ColorBlinker : MonoBehaviour
{
    public float FadeDuration = 1f;
    public Color Color1 = Color.gray;
    public Color Color2 = Color.white;
    public Image image;

    private Color startColor;
    private Color endColor;
    private float lastColorChangeTime;

    private Material material;

    void Start()
    {
        material = image.material;
        startColor = Color1;
        endColor = Color2;
    }

    void Update()
    {
        var ratio = (Time.time - lastColorChangeTime) / FadeDuration;
        ratio = Mathf.Clamp01(ratio);
        material.color = Color.Lerp(startColor, endColor, ratio);
        //material.color = Color.Lerp(startColor, endColor, Mathf.Sqrt(ratio)); // A cool effect
        //material.color = Color.Lerp(startColor, endColor, ratio * ratio); // Another cool effect

        if (ratio == 1f)
        {
            lastColorChangeTime = Time.time;

            // Switch colors
            var temp = startColor;
            startColor = endColor;
            endColor = temp;
        }
    }
}