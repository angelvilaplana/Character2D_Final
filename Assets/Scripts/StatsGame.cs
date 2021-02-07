using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class StatsGame : MonoBehaviour
{
    public TextPic txtStars;
    public TextPic txtGems;
    public TextPic txtJoinBlocks;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtStars.text = Star.NumStars.ToString();
        txtGems.text = Gem.NumGems.ToString();
        txtJoinBlocks.text = Gold.Together ? "0" : "1";
    }
}
