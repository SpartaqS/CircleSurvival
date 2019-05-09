using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{

    [SerializeField] List<Bomb> ActiveBombs;
    Stack<Bomb> InactiveBombs;

    [SerializeField] Bomb bombPrefab;
    [SerializeField] Canvas spawningCanvas;
    Bomb currentBomb;
    RectTransform currentBombRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 1; i++)
            BombPlace();
    }

    void BombPlace()
    {
        currentBomb = Instantiate(bombPrefab);
        currentBombRectTransform = currentBomb.GetComponent<RectTransform>();

        currentBomb.transform.SetParent(spawningCanvas.transform);
        currentBombRectTransform.anchoredPosition = GetRandomBombPosition();
        currentBomb.CircleReset(5f);
        ActiveBombs.Add(currentBomb);
    }

    void Update()
    {
        foreach(Bomb bomb in ActiveBombs)
        {
            bomb.ReduceDetonateTime(Time.deltaTime);
        }
    }

    Vector2 GetRandomBombPosition()
    {
        Vector2 tempVector = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        return tempVector;
    }

}
