using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

public class EndTimer : IInteraction
{
    UnityEvent onEnd;
    // Start is called before the first frame update
    public async Task Execute()
    {
        onEnd.Invoke();
    }

    public void QueueOnEnd(UnityEvent action)
    {
        this.onEnd = action;
        InteractionManager.Instance.AddCommand(this);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
