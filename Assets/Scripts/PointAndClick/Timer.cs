using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Timer : MonoBehaviour
{
    public void WaitSeconds(float seconds)
    {
        InteractionManager.Instance.AddInteraction(Task.Delay(TimeSpan.FromSeconds(seconds)));
    }
}
