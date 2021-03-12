using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChange : MonoBehaviour
{
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;
    public GameManager gameManager;

    public void ChageStage1()
    {
        SceneManager.LoadScene(1);
        GameManager.stageNum=1;

    }

    public void ChageStage2()
    {
        SceneManager.LoadScene(2);
        GameManager.stageNum=2;
    }

    public void ChageStage3()
    {
        SceneManager.LoadScene(3);
        GameManager.stageNum=3;
    }
}
