using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LucasArtsTalkTests
    {
        IMessageTalker talker;
        ITextTimeCalculator textTimeCalculator;

        [TearDown]
        public void TearDown()
        {
            InteractionManager.Instance.ClearAll();
        }

        [SetUp]
        public void SetUp()
        {
            InteractionManager.Instance.ClearAll();
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            textTimeCalculator = new TextTimeCalculator();
            talker = new LucasArtText(go.transform, textTimeCalculator);
        }

        // A Test behaves as an ordinary method
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Talk()
        {
            talker.Talk("hello world", false);
            yield return null;
            Assert.AreEqual("hello world", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkip()
        {
            talker.Talk("hello world", true);
            talker.Skip();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipToNextMessage()
        {
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
        public IEnumerator TalkAndSkipTwoMessages()
        {
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

        [UnityTest]
        public IEnumerator TalkAndTrySkipNonSkippable()
        {
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", false, false);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForEndOfFrame();
            InteractionManager.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("hello world", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndWaitToFinish()
        {
            talker.Talk("hello world", true);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world"));
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndWaitToFinishTwoMessages()
        {
            InteractionTalk inter1 = new InteractionTalk();
            inter1.Queue(talker, "hello world", false, false);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", false, false);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world") + textTimeCalculator.CalculateTime("message2"));
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndWaitToNextMessage()
        {
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", true, false);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world"));
            Assert.AreEqual("message2", talker.Text);
        }

    }
}
