using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var transformCamera = transform;
        var position = player.transform.position;
        transformCamera.position = new Vector3(position.x, position.y, transformCamera.position.z);
    }
}
