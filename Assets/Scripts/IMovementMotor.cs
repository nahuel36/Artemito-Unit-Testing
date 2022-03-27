using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementMotor 
{
    void Move(Vector3 direction, float currentSpeed);
}
