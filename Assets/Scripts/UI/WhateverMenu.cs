using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WhateverMenu : MonoBehaviour
{
    [System.Serializable]
    public class References
    {
        [SerializeField] private Button button;

        public Button Button => button;

        public References(Button button)
        {
            this.button = button;
        }
    }

    [SerializeField] References references;
    ITelemetrySender sender;
    // Start is called before the first frame update
    void Start()
    {
        var whatevermenucontroller = new WhateverMenuController();
        references.Button.onClick.AddListener(() => whatevermenucontroller.OnButtonClick());
    }

    public void Configure(References references)
    {
        this.references = references;
    }
    // Update is called once per frame

}
