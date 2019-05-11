using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BombManager : IBombManager // Dodac coroutine runnera zzeby mozna bylo robic z niego instantiate
{

    [SerializeField] List<GameObject> ActiveBombs = new List<GameObject>();
    [SerializeField] List<GameObject> InactiveBombs = new List<GameObject>();

    GameObject bombPrefab;
    Canvas spawningCanvas;
    ICoroutineRunner coroutineRunner;
    GameObject currentBomb;
    RectTransform currentBombRectTransform;

    public Vector3 ImageScale;

    internal int gameplayWidthMin,gameplayWidthMax,gameplayHeightMin,gameplayHeightMax;
    bool keepBombing;
    float bombInterval;

    Action endTheGame; // callback do wszystkiego ze gra sie skonczyla

    public BombManager(ICoroutineRunner coroutineRunner, GameObject bombPrefab, Canvas spawningCanvas, Action endGameHighscore)
    {
        Debug.Log("BombManager created");
        endTheGame += endGameHighscore;

        this.coroutineRunner = coroutineRunner;
        this.bombPrefab = bombPrefab;
        this.spawningCanvas = spawningCanvas;

        bombInterval = 2f;

     //   List<GameObject> ActiveBombs = new List<GameObject>();
    //    List<GameObject> InactiveBombs = new List<GameObject>();
    }

    public void StartRunning()
    {
        ImageScale = new Vector3(1, 1, 1);
        keepBombing = true;

        coroutineRunner.StartCoroutine(spawnBombs());
    }

    IEnumerator spawnBombs()
    {
        while (keepBombing)
        {
            BombPlace();
            bombInterval = Mathf.Max(0.2f, bombInterval - 0.1f);
            yield return new WaitForSeconds(bombInterval);
        }
    }

    public void BombPlace()
    {
        GameObject currentBomb = GameObject.Instantiate(bombPrefab);
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
                keepBombing = false;
                endTheGame.Invoke();

                foreach (GameObject temp in ActiveBombs)
                {
                    temp.GetComponent<Bomb>().isArmed = false; // tutaj mozna dodac ze szybciej bomby wybuchaja czy cos za pomoca detonateSpeedFactor
                }
                // tutaj trzeba dodac wybuch bomby, pokazanie czasu (callback do UI)
                Debug.Log(invokingBomb + " exploded");
                break;
            default:
                Debug.Log("Recieved unusual messageID: " + messageID);
                break;
        }
    }
}
