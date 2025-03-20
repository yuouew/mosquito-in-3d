using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour
{


    Rigidbody rig;


    public float magnitube = 0f;
    public GameObject smoke;
    public GameObject stun;


    public bool slowed;
    public bool stunned;

    float speed;


    public GameObject wingLA;
    public GameObject wingLB;

    public GameObject wingRA;
    public GameObject wingRB;

    float z = 5f;  //velocity

    private Vector3 savedPosition;

    // Start is called before the first frame update
    void Start()
    {
        savedPosition = transform.position;

        rig = GetComponent<Rigidbody>();

        smoke.SetActive(false);
        stun.SetActive(false);

        wingLA.SetActive(true);
        wingLB.SetActive(false);

        wingRA.SetActive(true);
        wingRB.SetActive(false);
    }

    public void Spawn()
    {
        transform.position = savedPosition;
    }

    //public void Bite()
    //{
    //    //transform.position = 
    //}

    public IEnumerator FlyOver(Vector3 targetPosition, float speed)
    {
        InvokeRepeating("TriggerLeft", 0f, 0.2f);
        InvokeRepeating("TriggerRight", 0f, 0.2f);

        while (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }

        CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            stun.SetActive(true);
            stun.transform.Rotate(new Vector3(0, 0, z)); //applying rotation
        }
        else
        {

            //KEY MOVEMENT

            ////flap left, push right
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    Left();

            //    wingLA.SetActive(false);
            //    wingLB.SetActive(true);
            //}
            //if (Input.GetKeyUp(KeyCode.A))
            //{
            //    wingLA.SetActive(true);
            //    wingLB.SetActive(false);
            //}

            ////flap right, push left
            //if (Input.GetKeyDown(KeyCode.L))
            //{
            //    Right();

            //    wingRA.SetActive(false);
            //    wingRB.SetActive(true);
            //}
            //if (Input.GetKeyUp(KeyCode.L))
            //{
            //    wingRA.SetActive(true);
            //    wingRB.SetActive(false);
            //}

            //FLAP MOVEMENT

            if (Input.GetKeyDown(KeyCode.A))
            {
                TriggerLeft();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                TriggerRight();
            }
        }

    }


    //STUN TRIGGER
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("dmg"))
        {
            //rig.velocity *= -speed;

            //rig.velocity = rig.velocity.normalized * ;

            ContactPoint contact = collision.contacts[0];  // Get the first contact point
            Vector3 normal = contact.normal;  // The normal of the surface we hit
            rig.velocity = Vector3.Reflect(rig.velocity, normal) * 5f;

            //stunned = true;

            StartCoroutine(StunTimer());
        }
    }

    IEnumerator StunTimer()
    {
        //waits one second
        yield return new WaitForSeconds(1f);

        //turns off stun effect
        stunned = false;
        stun.SetActive(false);

        //reset wing position
        wingLA.SetActive(true);
        wingLB.SetActive(false);

        wingRA.SetActive(true);
        wingRB.SetActive(false);
    }


    //SLOW TRIGGERS
    private void OnTriggerEnter(Collider other)
    {
        //on enter, turns on slowed bool, begins slow checking cycle
        if (other.gameObject.CompareTag("slow"))
        {
            slowed = true;
            CheckSlow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //turns off slowed bool, discontinueing slow checking cycle
        if (other.gameObject.CompareTag("slow"))
        {
            slowed = false;
        }
        //make slow last for two seconds, after end check again if collided to restart timer
        //on slow turn on particle effect, turn on game obj
    }

    //SLOWING
    private void CheckSlow()
    {
        //if slowed is on, will apply slow and begin a timer to check again in 2 seconds
        //otherwise will change back to normal
        if (slowed)
        {
            ApplySlow();
            StartCoroutine(SlowTimer());
        }
        else
        {
            magnitube = 2f;
            smoke.SetActive(false);
        }
    }

    private void ApplySlow()
    {
        magnitube = 1f;
        smoke.SetActive(true);
    }

    IEnumerator SlowTimer()
    {
        yield return new WaitForSeconds(2f);
        CheckSlow();
        //slowed = false;
    }

    ////WINGS
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

    IEnumerator ResetLeftWing()
    {
        yield return new WaitForSeconds(0.1f);
        wingLA.SetActive(true);
        wingLB.SetActive(false);
    }

    IEnumerator ResetRightWing()
    {
        yield return new WaitForSeconds(0.1f);
        wingRA.SetActive(true);
        wingRB.SetActive(false);
    }
}
