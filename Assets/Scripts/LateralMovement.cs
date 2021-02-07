using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralMovement : MonoBehaviour
{

    public Vector3 destination;
    public float time;
    
    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", destination,
            "time", time,
            "easeType", iTween.EaseType.easeInOutSine,
            "loopType", iTween.LoopType.pingPong));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
