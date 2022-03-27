using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhateverMenuController 
{
    readonly ITelemetrySender telemetrySender;

    public WhateverMenuController()
    {
        telemetrySender = ServiceLocator.Instance.GetService<ITelemetrySender>();

    }


    public void OnButtonClick()
    {
        telemetrySender.Send("ID");
    }
}
