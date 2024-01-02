using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccessUI : MonoBehaviour
{
    public TMP_Text textComponent;
    public float resetTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = Mathf.Round(Time.time - resetTime).ToString();
    }

    public void ResetTimer()
    {
        resetTime = Time.time;
    }
}
