using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PercentManager : MonoBehaviour
{
    public TMP_Text percentPlayerOne;
    public TMP_Text percentPlayerTwo;
    [SerializeField] public float scoreOne;
    [SerializeField]  public float scoreTwo;


    // Start is called before the first frame update
    void Start()
    {
        scoreOne = 0f;
        scoreTwo = 0f;
        percentPlayerOne.text = scoreOne.ToString() + "%";
        percentPlayerTwo.text = scoreTwo.ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {
        percentPlayerOne.text = scoreOne.ToString() + "%";
        percentPlayerTwo.text = scoreTwo.ToString() + "%";
    }
}
