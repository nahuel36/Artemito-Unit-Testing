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
            timer.WaitForSeconds(0.5f);
            while (InteractionManager.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("wait seconds test, duration:" + counter);
            Assert.GreaterOrEqual(counter, 0.5f);
        }

        [UnityTest]
        public IEnumerator InteractionManagerTestsAnidatedCalls()
        {
            float counter = 0;
            IteratedTimer timerIterated = new GameObject("Timer").AddComponent<IteratedTimer>();
            timerIterated.WaitForSecs(0.5f);
            
            while (InteractionManager.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("anidated Task test, duration:" + counter);
            Assert.GreaterOrEqual(counter, 0.5f);
        }
        
        [UnityTest]
        public IEnumerator InteractionManagerTwoInteractions()
        {
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.WaitForSeconds(0.5f);
            timer.WaitForSeconds(0.2f);
            while (InteractionManager.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("two wait seconds test, duration:" + counter);
            Assert.GreaterOrEqual(counter, 0.7f);
        }

        //recursive interaction
        //cancel interaction
    }
}
