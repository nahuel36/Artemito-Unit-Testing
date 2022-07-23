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
        [Test]
        public void TalkTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TalkTestsWithEnumeratorPasses()
        {
            GameObject go = new GameObject();
            go.AddComponent<TMPro.TextMeshProUGUI>();
            LucasArtText text = new LucasArtText(go.transform);
            text.Talk("hello world");
            yield return null;
            Assert.AreEqual("hello world", go.GetComponent<TMPro.TextMeshProUGUI>().text);
        }
    }
}
