using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DebugLogger : MonoBehaviour, ICommand
{
    public string log;

    public void DebugLog(string log)
    {
        this.log = log;
        CommandsQueue.Instance.AddCommand(this);
    }

    public async Task Execute()
    {
        //Debug.Log(log + " " + Time.time);
        ServiceLocator.Instance.GetService<ITelemetrySender>().Send(log + " " + Time.time);
        await Task.Yield();
    }

    public void Skip()
    {

    }
}

