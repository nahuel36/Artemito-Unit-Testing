using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Installer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var telemetrySender = new VoidTelemetrySenderAdapter(new UnityLog());
        ServiceLocator.Instance.RegisterService<ITelemetrySender>(telemetrySender);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
