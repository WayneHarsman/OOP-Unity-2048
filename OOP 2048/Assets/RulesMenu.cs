using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesMenu : MonoBehaviour
{
    public GameRules gameRules;

    public GameObject traditionalTooltip;
    public GameObject extendedTooltip;
    public GameObject endlessTooltip;

    public void SetGameState(int value)
    {
        gameRules.SetRule(value);

        if (value == 0)
        {
            traditionalTooltip.SetActive(true);
            extendedTooltip.SetActive(false);
            endlessTooltip.SetActive(false);
        }
        else if (value == 1)
        {
            traditionalTooltip.SetActive(false);
            extendedTooltip.SetActive(true);
            endlessTooltip.SetActive(false);

        }
        else if (value == 2)
        {
            traditionalTooltip.SetActive(false);
            extendedTooltip.SetActive(false);
            endlessTooltip.SetActive(true);

        }


    }

}
