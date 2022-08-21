using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEditor;
using Pathfinding;


namespace Tests
{
    public class CharacterAnimatorTests
    {
        PNCCharacter character;
        CharacterAnimatorInterface anim;

        [SetUp]
        public void SetUp()
        {

            GameObject characterGO = new GameObject("character");

            character = characterGO.AddComponent<PNCCharacter>();
            //Animator anim = characterGO.AddComponent<Animator>();
            anim = NSubstitute.Substitute.For<CharacterAnimatorInterface>();
            CharacterAnimator char_animator = characterGO.AddComponent<CharacterAnimator>();
            char_animator.Configure(anim, character);



        }

        [TearDown]
        public void TearDown()
        {
        }

        [UnityTest]
        public IEnumerator CharacterAnimatorTalk()
        {
            character.ConfigureTalker();
            character.Talk("Hello darling");
            
            yield return new WaitWhile(() => CommandsQueue.Instance.Executing());

            GameObject.Destroy(character.gameObject);
            anim.Received().SetTalking(true);
        }

        [UnityTest]
        public IEnumerator CharacterAnimatorWalk()
        {
            
            character.ConfigurePathFinder(5);

            GameObject pathfinder = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/PnC/Prefabs/PathFinder.prefab");
            GameObject pathfindergo = GameObject.Instantiate(pathfinder);

            character.Walk(new Vector3(5, -4, 0));
            
            yield return new WaitWhile(()=>CommandsQueue.Instance.Executing());
            GameObject.Destroy(pathfindergo);
            GameObject.Destroy(character.gameObject);
            anim.Received().SetWalking(true);
        }
    }
}
