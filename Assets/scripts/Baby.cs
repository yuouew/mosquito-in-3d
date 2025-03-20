using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Baby : MonoBehaviour
{

    public List<GameObject> locations = new List<GameObject>();

    public GameObject player;
    private KeyMovement kM;

    public GameObject baby;

    AudioSource audioSource;
    public List<AudioClip> sounds = new List<AudioClip>();

    //main menu
    public GameObject MM;
    public GameObject MMB1; //already baby
    public GameObject MMB2; //spawn baby
    public GameObject MMCam;
    public GameObject MMTitle;

    public GameObject LB;
    public GameObject LBEntry;
    //leaderboard button
    //turns off main menu
    //turns on leaderboard menu

    //leaderboard menu

    //back to menu
    //turns off leaderboard menu
    //turns on main menu

    //count 321
    //public string startTimer[] ;
    private string[] startTimer = new string[] { "Ready", "3", "2", "1", "Go!" };

    //timer start; when touch baby, timer stops
    public float scoreTimer;
    private bool countingScore = false;
    public TextMeshProUGUI currentScore;


    //ready, 3, 2, 1, go; win!!
    public TextMeshProUGUI bigText;
    //timer at top/bottom
    public TextMeshProUGUI smallText;

    private bool applyForce;
    private Vector3 savedPosition;
    private Quaternion savedRotation;



    // Start is called before the first frame update
    void Start()
    {

        savedPosition = baby.GetComponent<Rigidbody>().position;
        savedRotation = baby.GetComponent<Rigidbody>().rotation;

        //baby.GetComponent<Rigidbody>().AddForce(Vector3.back * 15f, ForceMode.Impulse);


        //player.SetActive(false);
        //player.GetComponent<KeyMovement>().Spawn();



        bigText.gameObject.SetActive(false);

        smallText.text = "0.00";
        scoreTimer = 0f;


        kM = player.GetComponent<KeyMovement>();
        kM.magnitube = 0f;


        MMButton2();

        //turn on mm
        //turn on mm title
        //turn off lb
        //turn off lb entry

    }


    // Update is called once per frame
    void Update()
    {
        if (applyForce)
        {
            baby.GetComponent<Rigidbody>().AddForce(Vector3.back * 5f, ForceMode.Force);
            baby.GetComponent<Rigidbody>().AddTorque(Vector3.left * 10f, ForceMode.Force);
        }

        if (countingScore)
        {
            scoreTimer += Time.deltaTime;
            smallText.text = scoreTimer.ToString("F2");
        }
    }

    //start ready timer, PAUSE MOVEMENT
    //timer ends, resume movement, ready timer inactive, score timer active start

    public void StartButton()
    {
        StartCoroutine(ReadyUp());
    }

    public void LBButton()
    {
        MMTitle.SetActive(true);
        MM.SetActive(false);

        MMB1.SetActive(true);
        MMB2.SetActive(false);

        LB.SetActive(true);
        LBEntry.SetActive(false);
    }

    //baby already there
    public void MMButton1()
    {
        MMTitle.SetActive(true);
        MM.SetActive(true);

        MMB1.SetActive(false);
        MMB2.SetActive(false);

        LB.SetActive(false);
        LBEntry.SetActive(false);
    }

    //baby respawn
    public void MMButton2()
    {
        MMTitle.SetActive(true);
        MM.SetActive(true);

        MMB1.SetActive(false);
        MMB2.SetActive(false);

        LB.SetActive(false);
        LBEntry.SetActive(false);


        baby.GetComponent<Rigidbody>().velocity = Vector3.zero;
        baby.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        baby.GetComponent<Rigidbody>().position = savedPosition;
        baby.GetComponent<Rigidbody>().rotation = savedRotation;
    }

    private IEnumerator ReadyUp()
    {
        //start button
        //turns off main menu
        //turns off main menu camera
        //turns on player
        //start game

        player.SetActive(false);
        player.GetComponent<KeyMovement>().Spawn();

        bigText.gameObject.SetActive(false);

        smallText.text = "0.00";
        scoreTimer = 0f;

        player.GetComponent<KeyMovement>().magnitube = 0f;


        MM.SetActive(false);
        MMCam.SetActive(false);
        player.SetActive(true);

        bigText.gameObject.SetActive(true);

        for (int i = 0; i < startTimer.Length; i++)
        {
            bigText.text = startTimer[i];

            if (startTimer[i] == startTimer[4])
            {
                GameStart();
            }

            yield return new WaitForSeconds(1f);
        }

        bigText.gameObject.SetActive(false);
    }

    private void GameStart()
    {
        countingScore = true;
        smallText.gameObject.SetActive(true);
        kM.magnitube = 3f;
    }


    //end game on contact with player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player.SetActive(false);
            MMCam.SetActive(true);

            countingScore = false;

            player.GetComponent<KeyMovement>().magnitube = 0f;

            //player.GetComponent<KeyMovement>().FlyOver(locations[2], 2f);


            //player touches end aoe
            //triggers baby deflation animation

            StartCoroutine(Drain());

        }
    }


    IEnumerator Drain()
    {

        yield return StartCoroutine(player.GetComponent<KeyMovement>().FlyOver(locations[1].transform.position, 1f));
        yield return new WaitForSeconds(1f);

        applyForce = true;
        StartCoroutine(player.GetComponent<KeyMovement>().FlyOver(locations[2].transform.position, 1f));
        yield return new WaitForSeconds(3f);

        applyForce = false;

        LB.SetActive(true);
        LBEntry.SetActive(true);
        MMTitle.SetActive(false);


        MMB1.SetActive(false);
        MMB2.SetActive(true);

        float multipliedScore = float.Parse(smallText.text) * 100;
        currentScore.text = Mathf.RoundToInt(multipliedScore).ToString();

        smallText.gameObject.SetActive(false);

        player.GetComponent<KeyMovement>().Spawn();



        //bug flies to preset location


        //bloody bite sound
        //straw sucking

        //baby drain anim
        //bug fly away??


        //open leaderboard



        //open to leaderboard with score and input field
        //input name to update leaderboard
        //back to menu button


    }
}
