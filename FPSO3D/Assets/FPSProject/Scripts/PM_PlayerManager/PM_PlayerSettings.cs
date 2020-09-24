using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PM_PlayerSettings
{
    #region
    [SerializeField, Header("Player name :")] string playerName = "Sebizart";
    [SerializeField, Header("Player life"), Range(1, 100)] int life = 100;
    [SerializeField, Header("Player power"), Range(0, 100)] float power = 100;
    #endregion

    #region Methods
    public string Name => playerName;
    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            life = IsDead() ? 0/*invok screen death, block input etc*/: life;
            life = life > 100 ? 100 : life;

        }
    }

    public float Power
    {
        get { return power; }
        set
        {
            power = value;
            power = power < 100 ?/*recharge power*/power : 100;
        }
    }

    public bool IsDead()
    {
        if (life <= 0)
            return true;
        return false;
    }

    public bool IsMaxPower()
    {
        return (power >= 100);
    }
    #endregion
}
