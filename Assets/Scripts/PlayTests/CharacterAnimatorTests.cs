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

        [UnityTest]
        public IEnumerator CharacterAnimatorTalk()
        {
            GameObject characterGO = new GameObject("character");
            PNCCharacter character = characterGO.AddComponent<PNCCharacter>();
            //Animator anim = characterGO.AddComponent<Animator>();
            var anim2 = NSubstitute.Substitute.For<CharacterAnimatorInterface>();
            CharacterAnimator char_animator = characterGO.AddComponent<CharacterAnimator>();
            char_animator.Configure(anim2, character);
            character.Talk("Hello darling");
            yield return null;
            anim2.Received().SetTalking(true);
        }

        [UnityTest]
        public IEnumerator CharacterAnimatorWalk()
        {
            GameObject pathfinder = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/PnC/Prefabs/PathFinder.prefab");
            GameObject.Instantiate(pathfinder);

            GameObject characterGO = new GameObject("character");
            AIPath path = characterGO.AddComponent<AIPath>();
            path.gravity = Vector3.zero;
            path.orientation = OrientationMode.YAxisForward;
            path.enableRotation = false;
            path.maxSpeed = 5;
            AIDestinationSetter setter = characterGO.AddComponent<AIDestinationSetter>();
            GameObject target = new GameObject("target");
            target.transform.position = characterGO.transform.position;
            setter.target = target.transform;
            PNCCharacter character = characterGO.AddComponent<PNCCharacter>();
            character.Configure(target);
            //Animator annim = characterGO.AddComponent<Animator>();
            var anim2 = NSubstitute.Substitute.For<CharacterAnimatorInterface>();
            CharacterAnimator char_animator = characterGO.AddComponent<CharacterAnimator>();
            char_animator.Configure(anim2, character);
            character.Walk(new Vector3(5, -4, 0));
            //target.transform.position = new Vector3(5, -4, 0);

            yield return new WaitForSeconds(1);
            anim2.Received().SetWalking(true);
        }
    }
}
