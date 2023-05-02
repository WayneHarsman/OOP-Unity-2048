using System;
using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileCell cell { get; private set; }

    public int value { get; private set; }
    public bool locked { get; set; }
    private GameRules rules;
    public event Action<int> OnStateChanged;

    private void Awake()
    {
        rules = GameObject.Find("Rule Manager").GetComponent<GameRules>();
    }

    public void SetState(int pValue)
    {
        if (rules.GetRule() == 2 && pValue > 2048)
        {
            value = 2048;
        }
        else
        {
            value = pValue;
        }
        OnStateChanged?.Invoke(pValue);
    }

    public void Spawn(TileCell pCell)
    {
        if (cell != null)
        {
            cell.tile = null;
        }

        cell = pCell;
        cell.tile = this;

        transform.position = pCell.transform.position;
    }

    public void MoveTo(TileCell pCell)
    {
        if (cell != null)
        {
            cell.tile = null;
        }

        cell = pCell;
        cell.tile = this;

        StartCoroutine(Animate(pCell.transform.position, false));
    }

    public void Merge(TileCell pCell)
    {
        if (cell != null)
        {
            cell.tile = null;
        }

        cell = null;
        pCell.tile.locked = true;

        StartCoroutine(Animate(pCell.transform.position, true));
    }

    private IEnumerator Animate(Vector3 pTo, bool pMerging)
    {
        float elapsed = 0f;
        float duration = 0.1f;

        Vector3 from = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, pTo, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = pTo;

        if (pMerging)
        {
            Destroy(gameObject);
        }
    }

}
