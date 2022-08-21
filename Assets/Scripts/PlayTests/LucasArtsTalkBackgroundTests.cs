using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LucasArtsTalkBackgroundTests
    {
        IMessageTalker talker;
        ITextTimeCalculator textTimeCalculator;
        CommandTalk talkInteraction;

        [TearDown]
        public void TearDown()
        {
            CommandsQueue.Instance.ClearAll();
            CommandsQueue.BackgroundInstance.ClearAll();
        }

        [SetUp]
        public void SetUp()
        {
            CommandsQueue.Instance.ClearAll();
            CommandsQueue.BackgroundInstance.ClearAll();
            GameObject go = new GameObject();
            textTimeCalculator = new TextTimeCalculator();
            talker = new LucasArtText(go.transform, textTimeCalculator);
            talkInteraction = new CommandTalk();
        }

        // A Test behaves as an ordinary method
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Talk()
        {
            talkInteraction.Queue(talker, "hello world", true, true);
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("hello world", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkip()
        {
            talkInteraction.Queue(talker, "hello world", true, true);
            yield return new WaitForEndOfFrame();
            CommandsQueue.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipToNextMessage()
        {
            CommandTalk inter = new CommandTalk();
            inter.Queue(talker, "hello world", true, true);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", true, true);
            yield return new WaitForEndOfFrame();
            CommandsQueue.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("message2", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipTwoMessages()
        {
            CommandTalk inter = new CommandTalk();
            inter.Queue(talker, "hello world", true, true);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", true, true);
            yield return new WaitForEndOfFrame();
            CommandsQueue.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            CommandsQueue.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndWaitToFinish()
        {
            talkInteraction.Queue(talker, "hello world", true, true);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world"));
            Assert.AreEqual("", talker.Text);
        }
        
        [UnityTest]
        public IEnumerator TalkAndWaitToFinishTwoMessages()
        {
            CommandTalk inter1 = new CommandTalk();
            inter1.Queue(talker, "hello world", false, true);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", false, true);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world") + textTimeCalculator.CalculateTime("message2"));
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }


        [UnityTest]
        public IEnumerator TalkAndWaitToNextMessage()
        {
            CommandTalk inter = new CommandTalk();
            inter.Queue(talker, "hello world", true, true);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", true, true);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world"));
            Assert.AreEqual("message2", talker.Text);
        }


        [UnityTest]
        public IEnumerator Z_TalkAndTrySkipNonSkippable()
        {
            CommandTalk inter = new CommandTalk();
            inter.Queue(talker, "hello world", false, true);
            CommandTalk inter2 = new CommandTalk();
            inter2.Queue(talker, "message2", true, true);
            yield return new WaitForEndOfFrame();
            CommandsQueue.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("hello world", talker.Text);
        }
    }
}
