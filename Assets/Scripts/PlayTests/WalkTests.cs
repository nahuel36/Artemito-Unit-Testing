using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using Pathfinding;

namespace Tests
{
    public class WalkTests
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
        }

        // A Test behaves as an ordinary method
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Walk()
        {
            GameObject pathfinder = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/PnC/Prefabs/PathFinder.prefab");
            GameObject.Instantiate(pathfinder);
            GameObject go = new GameObject("player");
            AIPath path = go.AddComponent<AIPath>();
            path.gravity = Vector3.zero;
            path.orientation = OrientationMode.YAxisForward;
            path.enableRotation = false;
            path.maxSpeed = 5;
            AIDestinationSetter setter = go.AddComponent<AIDestinationSetter>();
            GameObject target = new GameObject("target");
            target.transform.position = go.transform.position;
            setter.target = target.transform;
            yield return new WaitForEndOfFrame();
            target.transform.position = new Vector3(5, -4, 0);
            yield return new WaitUntil(() =>path.reachedEndOfPath);
            Assert.Less( Vector3.Distance(new Vector3(5, -4, 0), go.transform.position),0.25f);
        }

    }
}
