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
            CommandsQueue.Instance.ClearAll();
        }

        [SetUp]
        public void SetUp()
        {
            CommandsQueue.Instance.ClearAll();
            GameObject go = new GameObject();
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
            CommandTalk inter = new CommandTalk();
            inter.Queue(talker, "hello world", true, false);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForEndOfFrame();
            CommandsQueue.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("message2", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipTwoMessages()
        {
            CommandTalk inter = new CommandTalk();
            inter.Queue(talker, "hello world", true, false);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForEndOfFrame();
            CommandsQueue.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            CommandsQueue.Instance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndTrySkipNonSkippable()
        {
            CommandTalk inter = new CommandTalk();
            inter.Queue(talker, "hello world", false, false);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForEndOfFrame();
            CommandsQueue.Instance.SkipActualCommand();
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
            CommandTalk inter1 = new CommandTalk();
            inter1.Queue(talker, "hello world", false, false);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", false, false);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world") + textTimeCalculator.CalculateTime("message2"));
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndWaitToNextMessage()
        {
            CommandTalk inter = new CommandTalk();
            inter.Queue(talker, "hello world", true, false);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", true, false);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world"));
            Assert.AreEqual("message2", talker.Text);
        }

    }
}
