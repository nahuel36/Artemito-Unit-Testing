using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, IMovementMotor
{
    [SerializeField] float speed = 10;
    MovementController movementController;
    // Start is called before the first frame update
    void Awake()
    {
        movementController = new MovementController(this, speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        movementController.Move(horizontal, vertical);
    }

    public void Move(Vector3 direction, float currentSpeed)
    {
        transform.Translate(direction * (Time.fixedDeltaTime * speed));
    }

}
