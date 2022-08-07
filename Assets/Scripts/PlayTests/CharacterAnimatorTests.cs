using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class CharacterAnimatorTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void CharacterAnimatorTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CharacterAnimatorTestWithEnumeratorPasses()
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
    }
}
