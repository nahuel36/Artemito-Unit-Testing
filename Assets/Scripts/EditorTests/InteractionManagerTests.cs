using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;
using System;

namespace Tests
{
    public class InteractionManagerTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void InteractionManagerGetInstanceReturnNotNull()
        {
            // Use the Assert class to test conditions
            Assert.True(CommandsQueue.Instance != null);
        }
    }
}
