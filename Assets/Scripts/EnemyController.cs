using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public float xEndPosition;
    
    private float _xStartPosition;
    private bool _directionRight;
    
    // Start is called before the first frame update
    private void Start()
    {
        _xStartPosition = gameObject.transform.position.x;
        _directionRight = xEndPosition > _xStartPosition;
    }

    private void FixedUpdate()
    {
        var actualXPosition = gameObject.transform.position.x;
        if (_directionRight)
        {
            if (xEndPosition >= actualXPosition)
            {
                speed = Math.Abs(speed);
            }
            else
            {
                _directionRight = false;
            }
        }
        else
        {
            if (_xStartPosition <= actualXPosition)
            {
                speed = -Math.Abs(speed);
            }
            else
            {
                _directionRight = true;
            }
        }
        
        transform.localScale = speed < 0 ? new Vector3(.8f, .8f, 1) : new Vector3(-.8f, .8f, 1);
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (transform.position.y +  0.6f < other.transform.position.y)
            {
                other.SendMessage("EnemyJump");
                Destroy(gameObject);
            }
            else
            {
                other.SendMessage("EnemyKnockBack", transform.position.x);
            }
        }
    }
}
