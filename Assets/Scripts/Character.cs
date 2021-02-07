using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Character : MonoBehaviour
{

    public float speed = 0.0f;
    public float lateralMovement = 2.0f;
    public float jumpMovement = 5f;
    public AudioClip jumpAudio;
    public AudioClip gemCollect;
    public AudioClip starCollect;
    public AudioClip killEnemy;
    public AudioClip enemyKnock;
    public AudioClip boxJoin;
    public bool grounded = true;

    public Transform groundCheck;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private float _movementButton = 0.0f;
    private bool _enemyKnock;
    
    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _enemyKnock = false;
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics2D.Linecast(transform.position, 
            groundCheck.position, 
            1 << LayerMask.NameToLayer("Ground"));

        if (grounded && Input.GetButtonDown("Jump"))
        {
            _rigidbody2D.AddForce(Vector2.up * jumpMovement, ForceMode2D.Impulse);
            _audioSource.PlayOneShot(jumpAudio);
        }

        if (_movementButton != 0 && !_enemyKnock)
            speed = lateralMovement * _movementButton;
        else
            speed = 0;

        if (Input.GetAxis("Horizontal") != 0 && !_enemyKnock)
            speed = lateralMovement * Input.GetAxis("Horizontal");

        if (_enemyKnock)
        {
            _animator.SetTrigger("KnockBack");
            Invoke(nameof(Move), 1);
        }
        else if (grounded)
        {
            _animator.SetTrigger("Grounded");
        }
        else
        {
            _animator.SetTrigger("Jump");
        }

        transform.Translate(Vector2.right * (speed * Time.deltaTime));
        
        _animator.SetFloat("Speed", Mathf.Abs(speed));

        if (!_enemyKnock)
        {
            if (Input.GetAxis("Horizontal") > 0 || _movementButton > 0 )
            {
                transform.localScale = new Vector3(1, 1, 1);
            } else if (Input.GetAxis("Horizontal") < 0 || _movementButton < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("ZOOM"))
        {
            GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
        if (collider.gameObject.CompareTag("Gem"))
        {
            Gem.NumGems--;
            Destroy(collider.gameObject);
            _audioSource.PlayOneShot(gemCollect);
            
            if (Gem.NumGems == 0)
            {
                print("ALL GEMS COLLECTED !!!!");
            }
        } 
        else if (collider.gameObject.CompareTag("Star"))
        {
            Star.NumStars--;
            Destroy(collider.gameObject);
            _audioSource.PlayOneShot(starCollect);
            
            if (Star.NumStars == 0)
            {
                print("ALL STARS COLLECTED !!!!");
            }
        } 
        else if (collider.gameObject.name == "Door")
        {
            if (Gold.Together && Gem.NumGems == 0 && Star.NumStars == 0)
            {
                SceneManager.LoadScene(collider.GetComponent<Door>().sceneName);
            }
            else
            {
                print("THE DOOR IS CLOSED. YOU NEED TO COMPLETE THE GOALS !!!!");
            }
        }
        else if (Gold.Together && Gem.NumGems == 0 && Star.NumStars == 0)
        {
            print("ARCHIVED GOALS. GO TO THE DOOR !!!!");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("ZOOM"))
        {
            GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MobilePlatform"))
        {
            transform.SetParent(other.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MobilePlatform"))
        {
            transform.SetParent(null);
        }
    }

    private void Move()
    {
        _enemyKnock = false;
    }

    public void Jump()
    {
        if (grounded)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpMovement, ForceMode2D.Impulse);
            _audioSource.PlayOneShot(jumpAudio);
        }
    }

    public void EnemyJump()
    {
        _audioSource.PlayOneShot(killEnemy);
        _rigidbody2D.AddForce(Vector2.up * jumpMovement * 1.5f, ForceMode2D.Impulse);
    }

    public void EnemyKnockBack(float enemyPosX)
    {
        _audioSource.PlayOneShot(enemyKnock);
        _enemyKnock = true;
        _rigidbody2D.AddForce(Vector2.up * jumpMovement, ForceMode2D.Impulse);
        var side = Mathf.Sign(enemyPosX - transform.position.x);
        _rigidbody2D.AddForce(Vector2.left * side * 2, ForceMode2D.Impulse);
    }

    public void Move(float amount)
    {
        _movementButton = amount;
    }

    public void JoinGold()
    {
        if (!Gold.Together)
        {
            _audioSource.PlayOneShot(boxJoin);
        }
    }
}
