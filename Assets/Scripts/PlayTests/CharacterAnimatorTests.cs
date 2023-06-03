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

       [SetUp]
        public void SetUp()
        {




        }

        [TearDown]
        public void TearDown()
        {
        }

        [UnityTest]
        public IEnumerator CharacterAnimatorTalk()
        {
            GameObject pathfinder = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/PnC/Prefabs/PathFinder.prefab");
            GameObject pathfindergo = GameObject.Instantiate(pathfinder);


            GameObject characterGO = new GameObject("character");

            PNCCharacter character = characterGO.AddComponent<PNCCharacter>();
            character.forceAronPathFinder = true;
            character.forceTalkerLucasArts = true;
            character.dontConfigureAnimator = true;
            character.Initialize();
            //Animator anim = characterGO.AddComponent<Animator>();
            CharacterAnimatorInterface anim = NSubstitute.Substitute.For<CharacterAnimatorInterface>();
            CharacterAnimator char_animator = characterGO.AddComponent<CharacterAnimator>();
            char_animator.Configure(anim, character);

            character.Talk("Hello darling");
            
            yield return new WaitWhile(() => CommandsQueue.Instance.Executing());

            GameObject.Destroy(pathfindergo);
            GameObject.Destroy(character.gameObject);
            anim.Received().SetTalking(true);
        }

        [UnityTest]
        public IEnumerator CharacterAnimatorWalk()
        {
            GameObject pathfinder = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/PnC/Prefabs/PathFinder.prefab");
            GameObject pathfindergo = GameObject.Instantiate(pathfinder);


            GameObject characterGO = new GameObject("character");

            PNCCharacter character = characterGO.AddComponent<PNCCharacter>();
            character.forceAronPathFinder = true;
            character.forceTalkerLucasArts = true;
            character.dontConfigureAnimator = true;
            character.Initialize();
            //Animator anim = characterGO.AddComponent<Animator>();
            CharacterAnimatorInterface anim = NSubstitute.Substitute.For<CharacterAnimatorInterface>();
            CharacterAnimator char_animator = characterGO.AddComponent<CharacterAnimator>();
            char_animator.Configure(anim, character);

            character.Walk(new Vector3(5, -4, 0));
            
            yield return new WaitWhile(()=>CommandsQueue.Instance.Executing());
            GameObject.Destroy(pathfindergo);
            GameObject.Destroy(character.gameObject);
            anim.Received().SetWalking(true);
        }
    }
}
