using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

    public GameObject character;
    public static bool Together;
    
    // Start is called before the first frame update
    void Start()
    {
        Together = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gold"))
        {
            print("TWO COINS TOGETHER, OK !!!!");
            character.GetComponent<Character>().JoinGold();
            Together = true;
        }
    }
}
