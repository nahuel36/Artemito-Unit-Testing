using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITelemetrySender 
{
    void Send(string eventID);
}
