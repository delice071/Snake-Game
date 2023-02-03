using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class textt : MonoBehaviour
{
    public UnityEngine.UI.Text scoretext;

    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoretext.text = "" + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void yemekle()
    {
        score += 1;
        scoretext.text = "" + score;
    }
}
