using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneInitializer : MonoBehaviour
{
    [SerializeField] ScreenSpaceManager screenSpaceManager = null;
    [SerializeField] GameObject bombPrefab = null;
    [SerializeField] Canvas canvas = null;
    [SerializeField] GameObject timerAndDisplay;
    [SerializeField] GameObject coroutineRunnerPrefab;
    ICoroutineRunner coroutineRunner;
    void Start()
    {
        coroutineRunner = Instantiate(coroutineRunnerPrefab).GetComponent<ICoroutineRunner>();

        BombManager bombManager = new BombManager(coroutineRunner, bombPrefab, canvas, timerAndDisplay.GetComponent<TimerAndDisplay>().EndGameActions);
        screenSpaceManager.bombManager = bombManager;
        screenSpaceManager.CalculateGameplayArea(); // przesyla policzone dane do bombManagera
        timerAndDisplay.GetComponent<TimerAndDisplay>().ObtainCoroutineRunner(coroutineRunner);
        bombManager.StartRunning();
    }
}
