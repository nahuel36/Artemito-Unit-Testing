using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SumAndCounter : MonoBehaviour
{
    private ILog log;

    public SumAndCounter(ILog logParam)
    {
        log = logParam;
    }

    public int Suma(int value1, int value2)
    {
        if (value1 < 0 || value2 < 0)
            throw new System.Exception();

        int result = value1 + value2;

        log.Log($"{value1} + {value2} = {result}");

        return result;
    }

}
