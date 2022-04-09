using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class IteratedTimer : MonoBehaviour, IInteraction
{
    public Timer timer;

    public async Task Execute()
    {
        await timer.Execute();
    }

    public void WaitForSecs(float seconds)
    {
        Timer timer = new GameObject().AddComponent<Timer>();
        this.timer = timer;
        timer.WaitForSeconds(seconds);
    }
}
