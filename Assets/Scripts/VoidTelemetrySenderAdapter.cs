using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTelemetrySenderAdapter : ITelemetrySender
{
    public static ITelemetrySender Instance => instance ?? (instance = new VoidTelemetrySenderAdapter(new UnityLog()));
    static ITelemetrySender instance;
    readonly ILog log;

    public VoidTelemetrySenderAdapter(ILog Log)
    {
        this.log = Log;
    }

    public void Send(string eventID)
    {
        log.Log($"Send {eventID} event");
    }
}
