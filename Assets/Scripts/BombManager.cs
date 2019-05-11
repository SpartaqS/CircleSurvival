using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BombManager : MonoBehaviour , IBombManager
{

    [SerializeField] List<GameObject> ActiveBombs;
    [SerializeField] List<GameObject> InactiveBombs;

    [SerializeField] GameObject bombPrefab;
    [SerializeField] Canvas spawningCanvas;
    GameObject currentBomb;
    RectTransform currentBombRectTransform;

    public Vector3 ImageScale;

    internal int gameplayWidthMin,gameplayWidthMax,gameplayHeightMin,gameplayHeightMax;

    Action endGame; // callback do wszystkiego ze gra sie skonczyla

    // Start is called before the first frame update
    void Awake()
    {
        ImageScale = new Vector3(1, 1, 1);

        for (int i = 0; i < 20; i++)
            BombPlace();
    }

    void Update()
    {
        foreach (GameObject bomb in ActiveBombs)
        {
            bomb.GetComponent<Bomb>().ReduceDetonateTime(Time.deltaTime);
        }
    }

    public void BombPlace()
    {
        currentBomb = Instantiate(bombPrefab);
        currentBombRectTransform = currentBomb.GetComponent<RectTransform>();

        currentBomb.transform.SetParent(spawningCanvas.transform);
        currentBombRectTransform.anchoredPosition = GetRandomBombPosition();
        currentBomb.transform.localScale = ImageScale; // dopasowuje rozmair bomb do ekranu
        currentBomb.GetComponent<Bomb>().CircleReset(5f,GetBombStatus);
        ActiveBombs.Add(currentBomb);
    }

    Vector2 GetRandomBombPosition()
    {
        Vector2 tempVector = new Vector2(UnityEngine.Random.Range(gameplayWidthMin,gameplayWidthMax),
            UnityEngine.Random.Range(gameplayHeightMin,gameplayHeightMax));
        return tempVector;
    }

    internal void GetBombStatus(GameObject invokingBomb,int messageID)
    {
        switch (messageID)
        {
            case 0: //wylaczono bombe
                invokingBomb.SetActive(false);
                ActiveBombs.Remove(invokingBomb);
                InactiveBombs.Add(invokingBomb);

                break;
            case 1: //bomba wybuchla
                invokingBomb.SetActive(false);

                endGame.Invoke();
                // tutaj trzeba dodac wybuch bomby, pokazanie czasu (callback do UI)
                Debug.Log(invokingBomb + " exploded");
                break;
            default:
                Debug.Log("Recieved unusual messageID: " + messageID);
                break;
        }
    }
}
