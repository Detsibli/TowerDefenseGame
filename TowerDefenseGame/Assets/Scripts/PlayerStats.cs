using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 0;
    public static int Rounds;
    void Start()
    {
        Money = startMoney;
        Rounds = 0;
    }
}
