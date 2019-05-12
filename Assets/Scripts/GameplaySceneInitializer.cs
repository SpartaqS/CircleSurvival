using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneInitializer : MonoBehaviour
{
    [SerializeField] ScreenSpaceManager screenSpaceManager = null;
    [SerializeField] GameObject bombPrefab = null;
    [SerializeField] Canvas canvas = null;
    [SerializeField] GameObject timerAndDisplay = null;
    [SerializeField] GameObject coroutineRunnerPrefab = null;
    [SerializeField] GameObject explosionManager = null;
    ICoroutineRunner coroutineRunner;
    void Start()
    {
        coroutineRunner = Instantiate(coroutineRunnerPrefab).GetComponent<ICoroutineRunner>();

        BombManager bombManager = new BombManager(coroutineRunner, bombPrefab, canvas, timerAndDisplay.GetComponent<TimerAndDisplay>().EndGameActions,explosionManager.GetComponent<ExplosionManager>());
        screenSpaceManager.bombManager = bombManager;
        screenSpaceManager.CalculateGameplayArea(); // przesyla policzone dane do bombManagera
        timerAndDisplay.GetComponent<TimerAndDisplay>().ObtainCoroutineRunner(coroutineRunner);
        bombManager.StartRunning();
    }
}
