using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPool : IBombPool
{
    [SerializeField] List<GameObject> ActiveBombs;
    [SerializeField] Stack<GameObject> InactiveBombs = new Stack<GameObject>();

    GameObject bombPrefab;

    public BombPool(List<GameObject> ActiveBombs, GameObject bombPrefab)
    {
        this.ActiveBombs = ActiveBombs;
        this.bombPrefab = bombPrefab;
    }

    public GameObject TakeBombFromPool()
    {
        GameObject currentBomb = null;

        if (InactiveBombs.Count < 1)
        {
            currentBomb = GameObject.Instantiate(bombPrefab);
        }
        else
        {
            currentBomb = InactiveBombs.Pop();
            currentBomb.SetActive(true);
        }
        ActiveBombs.Add(currentBomb);
        return currentBomb;
    }
    public void ReturnBombToPool(GameObject currentBomb)
    {
        ActiveBombs.Remove(currentBomb);
        InactiveBombs.Push(currentBomb);
    }
}
