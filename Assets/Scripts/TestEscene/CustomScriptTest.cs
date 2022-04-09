using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class CustomScriptTest : CustomScript
{
    [SerializeField] Timer timer;

    public void Configure(Timer timer)
    {
        this.timer = timer;
    }

    public override void LoadScript()
    {
        timer.WaitForSeconds(0.2f);
        timer.WaitForSeconds(0.6f);
    }
}
