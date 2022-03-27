using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSubstitute;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests { 

    public class WhateverMenuTests 
    {
        ITelemetrySender telemetrySender;

        [SetUp]
        public void SetUp()
        {
            telemetrySender = Substitute.For<ITelemetrySender>();
            ServiceLocator.Instance.RegisterService(telemetrySender);
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Instance.Clear();
        }


        [Test]
        public void WhateverMenuControllerSimplePass()
        {
            //arrange
            WhateverMenuController whateverMenuController = new WhateverMenuController();

            //act
            whateverMenuController.OnButtonClick();

            //assert
            telemetrySender.Received(1).Send("ID");
        }

    }
}
