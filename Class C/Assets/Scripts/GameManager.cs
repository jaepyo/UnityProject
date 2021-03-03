using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    */

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public static int lifeNumber;
    // public int health;

    public PlayerMove player;
    public GameObject[] Stages;

    // public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;
    public Text UILife;

    public void Awake()
    {
        UILife.text = lifeNumber.ToString();
    }

    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        // Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
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

    public void LifeNumberUp()
    {
        // Player Die Effect
        //player.OnDie();
        // Retry Button UI
        //UIRestartBtn.SetActive(true);

        // Result UI
        //Debug.Log("Die");
        // Player Reposition
        Invoke("PlayerReposition", 0.5f);
        // LifeNumber up
        lifeNumber++;
        UILife.text = lifeNumber.ToString();
        // Restart Stage
        //Stages[stageIndex].SetActive(true);
        //PlayerPrefs.SetInt("lifeNumber", lifeNumber);
        SceneManager.LoadScene(0);

    }
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
        {
            PlayerReposition();
            // LifeNumber up
            LifeNumberUp();
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
