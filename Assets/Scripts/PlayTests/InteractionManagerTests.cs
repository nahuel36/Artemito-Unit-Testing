using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;
using System;
using NSubstitute;
using UnityEngine.Events;

namespace Tests
{
    public class InteractionManagerTests
    {
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        [Category("name")]
        public IEnumerator InteractionManagerTestsWaitSeconds()
        {
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.WaitForSeconds(0.5f);
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("wait seconds test, duration:" + counter);
            Assert.GreaterOrEqual(counter, 0.5f);
        }
        
        [UnityTest]
        public IEnumerator InteractionManagerThreeInteractions()
        {
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.WaitForSeconds(0.2f);
            timer.WaitForSeconds(0.5f);
            timer.WaitForSeconds(0.2f);
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();

            }
            Debug.Log("three wait seconds test, duration:" + counter);
            Assert.GreaterOrEqual(counter, 0.9f);
        }

        [UnityTest]
        public IEnumerator CustomScriptCall()
        {
            float counter = 0;
            CustomScriptTest cs = new GameObject("Custom").AddComponent<CustomScriptTest>();
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            cs.Configure(timer);
            cs.LoadScript();
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("custom script, duration:" + counter);
            Assert.GreaterOrEqual(counter, 0.8f);
        }

        [UnityTest]
        public IEnumerator CustomScriptCallAndMoreInteractions()
        {
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.WaitForSeconds(0.2f);
            CustomScriptTest cs = new GameObject("Custom").AddComponent<CustomScriptTest>();
            cs.Configure(timer);
            cs.LoadScript();
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("custom script and others, duration:" + counter);
            Assert.GreaterOrEqual(counter, 1f);
        }

        [UnityTest]
        public IEnumerator TimerStartAndEnd()
        {
            var OnEndSub = Substitute.For<Action>();
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.Configure(1.5f, OnEndSub);
            timer.StartTimer();
            while(counter < 1.6f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("Timer Duration:" + counter);
            OnEndSub.Received(1).Invoke();
        }

        [UnityTest]
        public IEnumerator TimerStartAndExecuteOtherAction()
        {
            
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.Configure(1.5f, ()=> { Debug.Log("executing end, duration:" + counter); counter += 1; });
            timer.StartTimer();
            while (counter < 1f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            timer.WaitForSeconds(3f);
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("Timer Duration:" + counter);
            Assert.GreaterOrEqual(counter, 5);
        }

        [UnityTest]
        public IEnumerator TimerStartAndConditionalEndTrue()
        {
            CommandsQueue.Instance.ClearConditionals();
            var OnEndSub = Substitute.For<Action>();
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.Configure(1.5f, OnEndSub);
            timer.StartTimer();
            while (counter < 1.6f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            timer.ifEnded();
            timer.WaitForSeconds(2);
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Timer Duration:" + counter);
            Assert.GreaterOrEqual(counter, 3.6f);
        }

        [UnityTest]
        public IEnumerator TimerStartAndConditionalEndFalse()
        {
            CommandsQueue.Instance.ClearConditionals();
            var OnEndSub = Substitute.For<Action>();
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.Configure(1.5f, OnEndSub);
            timer.StartTimer();
            while (counter < 1f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            timer.ifEnded();
            timer.WaitForSeconds(2);
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Timer Duration:" + counter);
            Assert.GreaterOrEqual(counter, 1f);
        }

        [UnityTest]
        public IEnumerator TimerStartAndConditionalAsIfEndFalse()
        {
            CommandsQueue.Instance.ClearConditionals();
            var OnEndSub = Substitute.For<Action>();
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.Configure(1.5f, OnEndSub);
            timer.StartTimer();
            while (counter < 1f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            if(timer.ifEnded())
                timer.WaitForSeconds(2);
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Timer Duration:" + counter);
            Assert.GreaterOrEqual(counter, 1f);
        }

        [UnityTest]
        public IEnumerator TimerStartUseConditionalFalseAndExecuteEndSub()
        {
            CommandsQueue.Instance.ClearConditionals();
            var OnEndSub = Substitute.For<Action>();
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            timer.Configure(1.5f, OnEndSub);
            timer.StartTimer();
            while (counter < 1f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            timer.ifEnded();
            while(counter < 2f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            CommandsQueue.Instance.DebugCount();
            Debug.Log("Timer Duration:" + counter);
            OnEndSub.Received(1).Invoke();
        }


        [UnityTest]
        public IEnumerator TimerStartUseConditionalFalseAndExecuteEndSubCustomScript()
        {
            CommandsQueue.Instance.ClearConditionals();
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            CustomScriptTest custom = new GameObject("Custom").AddComponent<CustomScriptTest>();
            custom.Configure(timer);
            timer.Configure(1.5f, ()=> { custom.LoadScript(); });//espera 0.8 seg
            timer.StartTimer();
            while (counter < 1f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            timer.ifEnded();
            while (counter < 2f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("Timer Duration:" + counter);
            Assert.GreaterOrEqual(counter, 2.3f);
            //en este caso, el clear conditionals al final de una ejecucion hace que se borren los condicionales
        }

        [UnityTest]
        public IEnumerator TimerStartUseConditionalFalseAndExecuteEndSubCustomScript2()
        {
            CommandsQueue.Instance.ClearConditionals();
            float counter = 0;
            Timer timer = new GameObject("Timer").AddComponent<Timer>();
            CustomScriptTest custom = new GameObject("Custom").AddComponent<CustomScriptTest>();
            custom.Configure(timer);
            timer.Configure(1.5f, () => { custom.LoadScript(); });//espera 0.8 seg
            timer.StartTimer();
            timer.WaitForSeconds(2f);
            while (counter < 1f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            timer.ifEnded();
            while (counter < 2f)
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            while (CommandsQueue.Instance.Executing())
            {
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("Timer Duration:" + counter);
            Assert.GreaterOrEqual(counter, 2.8f);
            //en este caso no se borran los condicionales porque llega al final, ya que antes de eso se encola otra tarea
            //sin embargo se borran los condicionales por ser un timer end
        }
        //cancel interaction
    }
}
