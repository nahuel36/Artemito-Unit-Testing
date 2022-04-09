using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

public class OnEndedTimer : IInteraction
{
    bool condition;

    // Start is called before the first frame update
    public async Task Execute()
    {
        InteractionManager.Instance.AddConditional(condition);
        await Task.Yield();
    }

    public void QueueConditional(bool condition)
    {
        this.condition = condition;
        InteractionManager.Instance.AddCommand(this);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
