using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TalkTests
    {
        // A Test behaves as an ordinary method
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Talk()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            LucasArtText text = new LucasArtText(go.transform, new TextTimeCalculator());
            text.Talk("hello world");
            yield return null;
            Assert.AreEqual("hello world", go.GetComponent<TMPro.TextMeshProUGUI>().text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkip()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            LucasArtText text = new LucasArtText(go.transform, new TextTimeCalculator());
            text.Talk("hello world", true);
            text.Skip();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", go.GetComponent<TMPro.TextMeshProUGUI>().text);
        }

        [UnityTest]
        public IEnumerator TalkAndWaitToFinish()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            ITextTimeCalculator calculator = new TextTimeCalculator();
            LucasArtText text = new LucasArtText(go.transform, calculator);
            text.Talk("hello world");
            yield return new WaitForSeconds(calculator.CalculateTime("hello world"));
            Assert.AreEqual("", go.GetComponent<TMPro.TextMeshProUGUI>().text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipToNextMessage()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            ITextTimeCalculator calculator = new TextTimeCalculator();
            LucasArtText talker = new LucasArtText(go.transform, calculator);
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", true, false);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForEndOfFrame();
            InteractionManager.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("message2", go.GetComponent<TMPro.TextMeshProUGUI>().text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipAllMessages()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            ITextTimeCalculator calculator = new TextTimeCalculator();
            LucasArtText talker = new LucasArtText(go.transform, calculator);
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", true, false);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForEndOfFrame();
            InteractionManager.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            InteractionManager.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", go.GetComponent<TMPro.TextMeshProUGUI>().text);
        }
    }
}
