using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearStage : MonoBehaviour
{
    public GameObject clear;

    public void Clear() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}
