using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class InteractionManager  
{
    private static InteractionManager instance;
    public static InteractionManager Instance {
        get
        {
            if(instance == null)
            {
                instance = new InteractionManager();
                instance.interactions = new Queue<Task>();
                instance.executing = false;
            }

            return instance;
        }
    }

    Queue<Task> interactions;
    bool executing;
    
    public void AddInteraction(Task task)
    {
        interactions.Enqueue(task);
        if(!executing)
        {
            executing = true;
            Execute();
        }
    }

    public bool Executing()
    {
        return executing;
    }

    async Task Execute()
    {
        while(interactions.Count > 0)
        {
            await interactions.Dequeue();
        }
        
        executing = false;
    } 
}
