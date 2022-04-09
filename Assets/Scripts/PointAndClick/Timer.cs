using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Timer : MonoBehaviour, IInteraction
{
    float seconds;

    /*public Timer(float seconds)
    {
        this.seconds = seconds;
    }*/
    void Configure(float seconds)
    {
        this.seconds = seconds;
    }

    public async Task Execute()
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
    }

    public void WaitForSeconds(float secondsP)
    {
        Configure(secondsP);
        InteractionManager.Instance.AddCommand(this);
    }

}
