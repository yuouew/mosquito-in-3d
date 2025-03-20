using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject FanBlades;

    public GameObject origin;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FanBlades.transform.Rotate(0, 0, 1500f * Time.deltaTime);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //find
            //rig.AddForce(Vector3.left * magnitube, ForceMode.Impulse);

            Rigidbody rb = other.GetComponent<Rigidbody>();

            ApplyForce(rb);
        }
    }


    void ApplyForce(Rigidbody body)
    {
        Vector3 direction = (body.transform.position - origin.transform.position).normalized;
        body.AddForce(direction * 20f, ForceMode.Force); // Adjust force magnitude as needed
    }

}
