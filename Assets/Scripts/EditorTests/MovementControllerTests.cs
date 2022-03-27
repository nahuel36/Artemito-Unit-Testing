using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Tests { 

    public class MovementControllerTests 
    {
        // Start is called before the first frame update
        [TestCase(1,0,1, new [] { 1,0,0})]
        [TestCase(-1, 0, 1, new[] {-1, 0, 0 })]
        [TestCase(0, 1, 1, new[] { 0, 1, 0 })]
        public void Move_CallTheMethod_CallToMotorMove(float horizontal, float vertical, float speed, int[] expected)
        {
            //arrange
            var movementMotor = Substitute.For<IMovementMotor>();
            var movementController = new MovementController(movementMotor, speed);

            //act
            movementController.Move(horizontal, vertical);

            //assert
            movementMotor.Received(1).Move(new Vector3(expected[0], expected[1],expected[2]), speed);
        }

    }
}
