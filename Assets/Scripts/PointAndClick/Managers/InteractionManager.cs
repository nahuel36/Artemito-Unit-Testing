using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class InteractionManager  
{
    public static InteractionManager Instance => _instance ?? (_instance = new InteractionManager());

    private readonly Queue<IInteraction> _commandsToExecute;
    private bool _runningCommand;
    private static InteractionManager _instance;

    private InteractionManager()
    {
        _commandsToExecute = new Queue<IInteraction>();
        _runningCommand = false;
    }

    public void AddCommand(IInteraction commandToEnqueue)
    {
        _commandsToExecute.Enqueue(commandToEnqueue);
        RunNextCommand();
    }

    public bool Executing()
    {
        return _runningCommand;
    }

    private async Task RunNextCommand()
    {
        if (_runningCommand)
        {
            return;
        }

        while (_commandsToExecute.Count > 0)
        {
            _runningCommand = true;
            var commandToExecute = _commandsToExecute.Dequeue();
            await commandToExecute.Execute();
        }

        _runningCommand = false;
    }
}
