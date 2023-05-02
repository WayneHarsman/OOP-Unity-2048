using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public enum Rules
    {
        TRADITIONAL,
        EXTENDED,
        ENDLESS
    }
    public Rules rule = Rules.TRADITIONAL;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetRule(int value)
    {
        rule = (Rules)value;
    }

    public int GetRule()
    {
        return (int)rule;
    }
}


