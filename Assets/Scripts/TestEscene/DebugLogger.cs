using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DebugLogger : MonoBehaviour, IInteraction
{
    public string log;

    public void DebugLog(string log)
    {
        this.log = log;
        InteractionManager.Instance.AddCommand(this);
    }

    public async Task Execute()
    {
        Debug.Log(log + " " + Time.time);
        await Task.Yield();
    }

    public void Skip()
    {

    }
}

