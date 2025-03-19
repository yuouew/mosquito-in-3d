using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public GameObject bugCam;
    public GameObject babCam;

    public GameObject end;




    // Start is called before the first frame update
    void Start()
    {
        bugCam.SetActive(true);
        babCam.SetActive(false);

        end.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bugCam.SetActive(false);
            babCam.SetActive(true);

            end.SetActive(true);
        }
    }

    private void Drained()
    {

    }
}
