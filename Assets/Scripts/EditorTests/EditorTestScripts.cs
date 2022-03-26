using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using NSubstitute;

namespace Tests
{
    public class NewTestScript
    {
        ILog logSubstitute;
        SumAndCounter script;

        [SetUp]
        public void SetUp()
        {
            logSubstitute = Substitute.For<ILog>();
            script = new SumAndCounter(logSubstitute);

        }

        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
            
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [TestCase(1,2,3)]
        [TestCase(2, 1, 3)]
        [TestCase(0, 0, 0)]
        public void Sum_TheParametersArePositive_ReturnTheResult(int value1, int value2, int expected)
        {
            // ARRANGE 
            //Script script = new Script(new UnityLog());

            //ACT
            int result = script.Suma(value1, value2);

            //ASERT
            Assert.AreEqual(expected, result);
        }


        [TestCase(-1,2)]
        [TestCase(1, -2)]
        [TestCase(-1, -2)]
        public void Sum_AnyParameterAreNegative_ThrowException(int value1, int value2)
        {
            Assert.Throws<Exception>(() =>
            {
                var result = script.Suma(value1, value2);
            });
        }

        [Test]
        public void Sum_TheParametersArePositive_WriteAnyLog()
        {
            script.Suma(5, 10);

            logSubstitute.Received().Log(Arg.Any<string>());
        }

        [TestCase(1, 2, 3)]
        [TestCase(2, 1, 3)]
        [TestCase(0, 0, 0)]
        public void Sum_TheParametersArePositive_LogResultAndParameters(int value1, int value2, int expected)
        {
            int result = script.Suma(value1, value2);

            logSubstitute.Received().Log(Arg.Is<string>(s => 
                                                            s.Contains(result.ToString()) &&
                                                            s.Contains(value1.ToString()) &&
                                                            s.Contains(value2.ToString())
                                                            ));

        }

        [Test]
        public void Count_ReturnsSpecificNumber()
        {
            logSubstitute.Count().Returns(23);
            Assert.AreEqual(23, logSubstitute.Count());
        }

        [Test]
        public void Count_DoSomeLogic()
        {
            logSubstitute.Count().Returns(info =>
            {
                Debug.Log("Do Some Logic");
                return 123;
            });

            Assert.AreEqual(123, logSubstitute.Count());

        }
    }
}
