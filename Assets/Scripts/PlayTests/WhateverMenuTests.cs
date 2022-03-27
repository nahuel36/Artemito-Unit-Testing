using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEditor;
using NSubstitute;

namespace Tests {

    public class WhateverMenuTests
    {
        ITelemetrySender telemetrySender;

        [SetUp]
        public void SetUp()
        {
            telemetrySender = Substitute.For<ITelemetrySender>();
            ServiceLocator.Instance.RegisterService<ITelemetrySender>(telemetrySender);
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Instance.Clear();
        }

        [UnityTest]
        public IEnumerator WhateverTestWithEnumeratorPasses()
        {
            //arrange            
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/WhateverMenu.prefab");
            var instance = Object.Instantiate(prefab);
            

            yield return null;

            //act
            instance.GetComponentInChildren<Button>().onClick.Invoke();

            //assert
            telemetrySender.Received(1).Send("ID");
        }


        [UnityTest]
        public IEnumerator WhateverTestWithEnumeratorPasses2()
        {
            //arrange            
            var gameobject = new GameObject();
            var whatevermenu = gameobject.AddComponent<WhateverMenu>();
            var button = gameobject.AddComponent<Button>();
            var references = new WhateverMenu.References(button);
            whatevermenu.Configure(references);

            yield return null;

            //act
            button.onClick.Invoke();

            //assert
            telemetrySender.Received(1).Send("ID");
        }

    }
}
