using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public string sceneName;
    public Sprite doorOpened;
    private SpriteRenderer _door;
    
    // Start is called before the first frame update
    void Start()
    {
        _door = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gold.Together && Gem.NumGems == 0 && Star.NumStars == 0)
        {
            _door.sprite = doorOpened;
        } 
    }

}
