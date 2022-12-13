using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birillo : MonoBehaviour
{
    
    private readonly int score = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GetComponent<SphereCollider>().enabled = false;
            GameController.instance.total += score;
            GameController.instance.UpdateScoreText();
            Invoke("SetInvisible", 0.55f);
        }
    }
    private void SetInvisible()
    {
        gameObject.SetActive(false);
    }
}
