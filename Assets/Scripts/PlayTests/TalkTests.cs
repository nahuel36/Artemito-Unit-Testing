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
            IMessageTalker talker = new LucasArtText(go.transform, new TextTimeCalculator());
            talker.Talk("hello world", false);
            yield return null;
            Assert.AreEqual("hello world", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkip()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            IMessageTalker talker = new LucasArtText(go.transform, new TextTimeCalculator());
            talker.Talk("hello world", true);
            talker.Skip();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndWaitToFinish()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            ITextTimeCalculator calculator = new TextTimeCalculator();
            IMessageTalker talker = new LucasArtText(go.transform, calculator);
            talker.Talk("hello world", true);
            yield return new WaitForSeconds(calculator.CalculateTime("hello world"));
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipToNextMessage()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            ITextTimeCalculator calculator = new TextTimeCalculator();
            IMessageTalker talker = new LucasArtText(go.transform, calculator);
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", true, false);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForEndOfFrame();
            InteractionManager.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("message2", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipAllMessages()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            ITextTimeCalculator calculator = new TextTimeCalculator();
            IMessageTalker talker = new LucasArtText(go.transform, calculator);
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", true, false);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForEndOfFrame();
            InteractionManager.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            InteractionManager.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }
    }
}
