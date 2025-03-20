using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Baby : MonoBehaviour
{

    public List<Vector3> locations = new List<Vector3>();

    public GameObject player;
    private KeyMovement kM;

    //main menu
    public GameObject MM;
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


    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        player.GetComponent<KeyMovement>().Spawn();



        bigText.gameObject.SetActive(false);

        smallText.text = "0.00";
        scoreTimer = 0f;


        kM = player.GetComponent<KeyMovement>();
        kM.magnitube = 0f;


        MMButton();

        //turn on mm
        //turn on mm title
        //turn off lb
        //turn off lb entry

    }

    // Update is called once per frame
    void Update()
    {
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
        LB.SetActive(true);
        LBEntry.SetActive(false);
    }

    public void MMButton()
    {
        MMTitle.SetActive(true);
        MM.SetActive(true);
        LB.SetActive(false);
        LBEntry.SetActive(false);

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
    private void OnCollisionEnter(Collision other)
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

        yield return StartCoroutine(player.GetComponent<KeyMovement>().FlyOver(locations[1], 1f));
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(player.GetComponent<KeyMovement>().FlyOver(locations[2], 1f));

        LB.SetActive(true);
        LBEntry.SetActive(true);
        MMTitle.SetActive(false);


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
