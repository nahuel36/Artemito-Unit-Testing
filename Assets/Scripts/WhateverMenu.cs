using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhateverMenu : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button button;

    // Start is called before the first frame update
    void Awake()
    {
        button.onClick.AddListener(SendTelemetry);
    }


    // Update is called once per frame
    void SendTelemetry()
    {
        VoidTelemetrySenderAdapter.Instance.Send("ID");
    }
}
