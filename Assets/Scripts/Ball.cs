using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float force;
    // Use this for initialization
    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;
    public GameObject MenuUI;
    public GameObject StrikeUI;
    public GameObject SpareUI;
    private bool trowing;
    private bool menu;
    private GameObject[] pins;
    void Start()
    {
        trowing = false;
        menu = false;
        pins = GameObject.FindGameObjectsWithTag("Pin");
        pinPositions = new List<Vector3>();
        pinRotations = new List<Quaternion>();
        foreach (var pin in pins)
        {
            pinPositions.Add(pin.transform.position);
            pinRotations.Add(pin.transform.rotation);
        }

        ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Space)) { 
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -(force * Time.deltaTime)), ForceMode.VelocityChange);
            trowing = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        if (Input.GetKeyUp(KeyCode.R))
        {
            Strike();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            Spare();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {

            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        if (GetComponent<Rigidbody>().IsSleeping() && trowing)
        {
            var pins2 = GameObject.FindGameObjectsWithTag("Pin");
            MenuUI.SetActive(true);
            if(menu == false)
            {
                if (pins2.Length > 0)
                {
                    SpareUI.SetActive(true);
                    menu = true;
                }
                else {
                    StrikeUI.SetActive(true);
                    menu = true;
                }
                    
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pin"))
        {
            GetComponent<AudioSource>().Play();

            
        }
            
    }    // Start is called before the first frame update
    public void Spare()
    {
        var ball = GameObject.FindGameObjectWithTag("Ball");
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.position = ballPosition;
        trowing = false;
        SpareUI.SetActive(false);
        MenuUI.SetActive(false);
        menu = false;
    }
    public void Strike()
    {
        
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].SetActive(true);
            pins[i].GetComponent<SphereCollider>().enabled = true;
        }
        var pins1 = GameObject.FindGameObjectsWithTag("Pin");
        for (int i = 0; i < pins1.Length; i++)
        {
            //collision.gameObject.transform.parent.gameObject.tag
            var pinPhysics = pins1[i].GetComponent<Rigidbody>();
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.position = pinPositions[i];
            pinPhysics.rotation = pinRotations[i];
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.angularVelocity = Vector3.zero;
            
            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.transform.position = ballPosition;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        trowing = false;
        StrikeUI.SetActive(false);
        MenuUI.SetActive(false);
        menu = false;
    }
}

    
