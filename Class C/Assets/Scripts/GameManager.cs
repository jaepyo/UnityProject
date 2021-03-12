using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
   
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
<<<<<<< Updated upstream
    public static int lifeNumber;
    public static int stageNum;
=======
>>>>>>> Stashed changes
    // public int health;

    public static bool checkPoint = false;
    public CheckPoint checkPointBody;

    public PlayerMove player;
    public GameObject[] Stages;
    public Number1 num1;

    // public Image[] UIhealth;
    public Text UIPoint;
    //public Text UIStage;
    public GameObject UIRestartBtn;

<<<<<<< Updated upstream
    public void Awake()
    {
        UILife.text = lifeNumber.ToString();
        if (checkPoint)
        {
            player.transform.position = checkPointBody.transform.position;
            //player.VelocityZero();
        }
        else
        {
            player.transform.position = new Vector3(0, 0, -1);
            //player.VelocityZero();
        }
    }
=======
    
>>>>>>> Stashed changes

    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        // Change Stage
        stageNum++;
        if (stageNum < stageIndex)
        {
            /*
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
            */
            //UIStage.text = "STAGE " + (stageNum + 1);
            SceneManager.LoadScene(stageNum);
            lifeNumber = 0;
            checkPoint = false;
        }
        else
        { // Game Claer
            // Player Control Lock
            Time.timeScale = 0;
            // Restart Button UI
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Clear!";
            UIRestartBtn.SetActive(true);
        }

        // Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }

   // public void LifeNumberUp()
   // {
        // Player Die Effect
        //player.OnDie();
        // Retry Button UI
        //UIRestartBtn.SetActive(true);

        // Result UI
        //Debug.Log("Die");
        // Player Reposition
       // Invoke("PlayerReposition", 0.1f);
        // LifeNumber up
      //  lifeNumber++;
       // UILife.text = lifeNumber.ToString();
        // Restart Stage
<<<<<<< Updated upstream
        //Stages[stageIndex].SetActive(true);
        //PlayerPrefs.SetInt("lifeNumber", lifeNumber);
        SceneManager.LoadScene(stageNum);
    }
=======
       // Stages[stageIndex].SetActive(true);
        //PlayerPrefs.SetInt("lifeNumber", lifeNumber);
        //SceneManager.LoadScene(0);

  //  }
>>>>>>> Stashed changes
    /*
    public void HealthDown()
    {
        if (health > 1) {
        health--;
        UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            // All Health UI Off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            // Player Die Effect
            player.OnDie();
            // Result UI
            Debug.Log("쭉엇쓰");
            // Retry Button UI
            UIRestartBtn.SetActive(true);
        }
    }
    */
    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 마지막 죽음이 낭떠러지일때 원위치 시키지 않기
            // Player Reposition
            if (health > 1) {
                PlayerReposition();
            }

            // Health Down
            HealthDown();
        }
    }
    */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
<<<<<<< Updated upstream
        {
            PlayerReposition();
=======
        { 
           // LifeNumberUp();
          
            PlayerReposition();
            num1.Up1();
            Restart();
            
            
            
>>>>>>> Stashed changes
            // LifeNumber up
            
        }
    }

    void PlayerReposition()
    {
            player.transform.position = new Vector3(0, 0, -1);
            player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
