using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Number1 : MonoBehaviour
{
    // Start is called before the first frame update
    public static int LifeNumber; // 횟수저장하기위한변수
    public  Text UINumber;

    public void Awake()
    {   
        UINumber.text=LifeNumber.ToString();

    }

    public void Up1()
    {

        LifeNumber++; // 죽었을때 횟수 올라감

    }
}
