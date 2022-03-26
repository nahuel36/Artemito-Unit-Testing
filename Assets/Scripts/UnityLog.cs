using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityLog : ILog
{
    // Start is called before the first frame update
    public void Log(string message)
    {
        Debug.Log(message);
    }

    public int Count()
    {
        return 22;
    }
}
