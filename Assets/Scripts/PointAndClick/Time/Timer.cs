﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;

public class Timer : MonoBehaviour//, IInteraction
{
    float seconds;
    float counter;
    bool started;

    public bool Started { set { started = value; } }

    bool ended;
    [SerializeField] UnityEvent onEnd;

    public void StartTimer()
    {
        StartTimer start = new StartTimer();
        start.Queue(this);
    }

    public void Update()
    {
        if(started)
        {
            if (!ended)
            { 
                counter += Time.deltaTime;
                if(counter > seconds)
                {
                    ended = true;
                    onEnd.Invoke();
                }
            }
        }
    }

    
    public void Configure(float seconds, Action endAction)
    {
        this.seconds = seconds;
        this.onEnd = new UnityEvent();
        this.onEnd.AddListener( () => endAction.Invoke());
    }
    /*
    public async Task Execute()
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
    }
    */
    public void WaitForSeconds(float secondsP)
    {
        TimerEfimero efimero = new TimerEfimero();
        efimero.WaitForSeconds(secondsP);
    }

}
