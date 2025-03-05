using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    Rigidbody rig;


    float magnitube;

    bool slowed;

    float speed;


    public GameObject wingLA;
    public GameObject wingLB;

    public GameObject wingRA;
    public GameObject wingRB;



    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        Vector3 vel = rig.velocity;



        wingLA.SetActive(true);
        wingLB.SetActive(false);

        wingRA.SetActive(true);
        wingRB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (slowed)
        {
            magnitube = 1f;
        }
        else
        {
            magnitube = 2f;
        }

        //speed = rig.velocity.magnitude;


        if (Input.GetKeyDown(KeyCode.A))
        {
            Left();

            wingLA.SetActive(false);
            wingLB.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            wingLA.SetActive(true);
            wingLB.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Right();

            wingRA.SetActive(false);
            wingRB.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            wingRA.SetActive(true);
            wingRB.SetActive(false);
        }



    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("dmg"))
        {
            //rig.velocity *= -speed;

            //rig.velocity = rig.velocity.normalized * ;

            ContactPoint contact = collision.contacts[0];  // Get the first contact point
            Vector3 normal = contact.normal;  // The normal of the surface we hit
            rig.velocity = Vector3.Reflect(rig.velocity, normal) * 5f;
        }


    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("slow"))
        {
            slowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

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
}
