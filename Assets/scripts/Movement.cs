using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rig;
    float magnitube = 5;

    public GameObject wingLA;
    public GameObject wingLB;
    public GameObject wingRA;
    public GameObject wingRB;

    void Start()
    {
        rig = GetComponent<Rigidbody>();

        wingLA.SetActive(true);
        wingLB.SetActive(false);
        wingRA.SetActive(true);
        wingRB.SetActive(false);
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.A))
        {
            TriggerLeft();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            TriggerRight();
        }
    }

   
    public void TriggerLeft()
    {
        Debug.Log("Left Triggered!");
        wingLA.SetActive(false);
        wingLB.SetActive(true);
        Left(); 
        StartCoroutine(ResetLeftWing());
    }

    public void TriggerRight()
    {
        Debug.Log("Right Triggered!");
        wingRA.SetActive(false);
        wingRB.SetActive(true);
        Right(); 
        StartCoroutine(ResetRightWing()); 
    }

    private void Right()
    {
        rig.AddForce(Vector3.forward * magnitube, ForceMode.Impulse);
        rig.AddForce(Vector3.left * magnitube, ForceMode.Impulse);
    }

    private void Left()
    {
        rig.AddForce(Vector3.forward * magnitube, ForceMode.Impulse);
        rig.AddForce(Vector3.right * magnitube, ForceMode.Impulse);
    }

    IEnumerator ResetLeftWing()
    {
        yield return new WaitForSeconds(0.2f);  
        wingLA.SetActive(true);
        wingLB.SetActive(false);
    }

    IEnumerator ResetRightWing()
    {
        yield return new WaitForSeconds(0.2f); 
        wingRA.SetActive(true);
        wingRB.SetActive(false);
    }
}

