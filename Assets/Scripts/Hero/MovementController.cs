using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController 
{
    [SerializeField] float speed = 10;
    IMovementMotor movementMotor;

    public MovementController(IMovementMotor movementMotor, float speed)
    {
        this.speed = speed;
        this.movementMotor = movementMotor;
    }

    public void Move(float horizontal, float vertical)
    {
        var direction = new Vector3(horizontal, vertical, 0).normalized;
        movementMotor.Move(direction, speed);
    }
}
