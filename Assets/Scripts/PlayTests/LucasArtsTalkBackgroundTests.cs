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
        InteractionTalk talkInteraction;

        [TearDown]
        public void TearDown()
        {
            InteractionManager.Instance.ClearAll();
            InteractionManager.BackgroundInstance.ClearAll();
        }

        [SetUp]
        public void SetUp()
        {
            InteractionManager.Instance.ClearAll();
            InteractionManager.BackgroundInstance.ClearAll();
            GameObject go = new GameObject();
            textTimeCalculator = new TextTimeCalculator();
            talker = new LucasArtText(go.transform, textTimeCalculator);
            talkInteraction = new InteractionTalk();
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
            InteractionManager.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipToNextMessage()
        {
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", true, true);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, true);
            yield return new WaitForEndOfFrame();
            InteractionManager.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("message2", talker.Text);
        }

        [UnityTest]
        public IEnumerator TalkAndSkipTwoMessages()
        {
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", true, true);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, true);
            yield return new WaitForEndOfFrame();
            InteractionManager.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            InteractionManager.BackgroundInstance.SkipActualCommand();
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
            InteractionTalk inter1 = new InteractionTalk();
            inter1.Queue(talker, "hello world", false, true);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", false, true);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world") + textTimeCalculator.CalculateTime("message2"));
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("", talker.Text);
        }


        [UnityTest]
        public IEnumerator TalkAndWaitToNextMessage()
        {
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", true, true);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, true);
            yield return new WaitForSeconds(textTimeCalculator.CalculateTime("hello world"));
            Assert.AreEqual("message2", talker.Text);
        }


        [UnityTest]
        public IEnumerator Z_TalkAndTrySkipNonSkippable()
        {
            InteractionTalk inter = new InteractionTalk();
            inter.Queue(talker, "hello world", false, true);
            InteractionTalk inter2 = new InteractionTalk();
            inter2.Queue(talker, "message2", true, true);
            yield return new WaitForEndOfFrame();
            InteractionManager.BackgroundInstance.SkipActualCommand();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual("hello world", talker.Text);
        }
    }
}
