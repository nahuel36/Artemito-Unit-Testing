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
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator InteractionManagerTestsWaitSeconds()
        {
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.WaitSeconds(0.5f);
            while (InteractionManager.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log(counter);
            Assert.GreaterOrEqual(counter, 0.5f);
        }

        //recursive interaction
        //cancel interaction
    }
}
