using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public float percent;
    [SerializeField] float Stock;
    private PercentManager percentManager;
    // Start is called before the first frame update
    void Start()
    {
        percentManager = GameObject.Find("Canvas").GetComponent<PercentManager>();
        percent = 0;
        Stock = 3;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // if (collision.tag == )
    }
    void Update()
    {
        //percentManager.scoreOne += 1f;
    }
}
