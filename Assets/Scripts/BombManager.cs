using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CircleSurvival
{
    public class BombManager : IBombManager
    {
        [SerializeField] List<GameObject> ActiveBombs = new List<GameObject>();

        GameObject bombPrefab;
        Canvas spawningCanvas;
        ICoroutineRunner coroutineRunner;
        GameObject currentBomb;
        RectTransform currentBombRectTransform;
        BombPool bombPool;
        ExplosionManager explosionManager;

        public Vector3 ImageScale;

        internal int gameplayWidthMin, gameplayWidthMax, gameplayHeightMin, gameplayHeightMax;
        bool keepBombing;
        float bombInterval, bombTimerMin, bombTimerMax, nonDisarmableTimer;

        Action endTheGame; // callback do wszystkiego ze gra sie skonczyla

        public BombManager(ICoroutineRunner coroutineRunner, GameObject bombPrefab, Canvas spawningCanvas, Action endGameHighscore, ExplosionManager explosionManager)
        {
            endTheGame += endGameHighscore;

            this.coroutineRunner = coroutineRunner;
            this.bombPrefab = bombPrefab;
            this.spawningCanvas = spawningCanvas;
            this.explosionManager = explosionManager;

            bombInterval = Constants.STARTING_BOMB_INTERVAL;
            bombTimerMin = Constants.STARTING_MIN_TIMER;
            bombTimerMax = Constants.STARTING_MAX_TIMER;
            nonDisarmableTimer = Constants.NON_DISARMABLE_TIMER;

            bombPool = new BombPool(ActiveBombs, bombPrefab);
        }

        public void StartRunning()
        {
            ImageScale = new Vector3(1, 1, 1);
            keepBombing = true;

            coroutineRunner.StartCoroutine(spawnBombs());
        }

        IEnumerator spawnBombs() //poziom trudnosci kalibrujemy tu
        {
            while (keepBombing)
            {
                BombPlace(bombTimerMin, bombTimerMax);
                bombInterval = Mathf.Max(Constants.MINIMUM_BOMB_INTERVAL, bombInterval - 0.025f * bombInterval);

                if (bombInterval < Constants.MINIMUM_BOMB_INTERVAL + 0.1f)
                {
                    ReduceTimer();
                }

                Debug.Log("bombInterval: " + bombInterval);

                yield return new WaitForSeconds(bombInterval);
            }
        }

        public void BombPlace(float bombTimerMin, float bombTimerMax)
        {
            Vector2 possibleCoordinates = GetRandomBombPosition(0);

            if (possibleCoordinates.x > 0) // failsafe aby nie szukac nowego miejsca w nieskonczonosc
            {
                GameObject currentBomb = bombPool.TakeBombFromPool();
                currentBombRectTransform = currentBomb.GetComponent<RectTransform>();

                currentBomb.transform.SetParent(spawningCanvas.transform);
                currentBomb.transform.SetAsFirstSibling();                
                currentBombRectTransform.anchoredPosition = possibleCoordinates;
                BombType bombType = GetRandomBombType();
                currentBomb.transform.localScale = ImageScale; // dopasowuje rozmiar bomb do ekranu
                currentBomb.GetComponent<Bomb>().CircleReset(bombType == 0 ? UnityEngine.Random.Range(bombTimerMin, bombTimerMax) : nonDisarmableTimer, GetBombStatus, bombType, explosionManager.DoExplosionEffect);
            }
            else // "kara" zeby nie oplacalo sie trzymac calego ekranu w bombach
                ReduceTimer();
        }

        Vector2 GetRandomBombPosition(int triesTaken)
        {
            if (triesTaken > 20)
            {
                return new Vector2(-1, -1);
            }

            Vector2 tempVector = new Vector2(UnityEngine.Random.Range(gameplayWidthMin, gameplayWidthMax),
                UnityEngine.Random.Range(gameplayHeightMin, gameplayHeightMax));

            if (ActiveBombs.Count > 0)
            {
                foreach (GameObject currentBomb in ActiveBombs)
                {
                    currentBombRectTransform = currentBomb.GetComponent<RectTransform>();
                    float distanceToCurrentBomb = Mathf.Sqrt(Mathf.Pow((currentBombRectTransform.anchoredPosition.x - tempVector.x), 2)
                        + Mathf.Pow((currentBombRectTransform.anchoredPosition.y - tempVector.y), 2));
                    if (distanceToCurrentBomb < 130f) // promien bomby to 64
                    {
                        triesTaken++;
                        return GetRandomBombPosition(triesTaken);
                    }
                }
            }
            return tempVector;
        }

        BombType GetRandomBombType()
        {
            float randomizer = UnityEngine.Random.Range(0f, 1f);

            if (randomizer <= Constants.NON_DISARMABLE_CHANCE)
                return BombType.nonDisarmable;
            else
                return BombType.disarmable;
        }

        internal void GetBombStatus(GameObject invokingBomb, BombType messageID)
        {
            switch (messageID)
            {
                case BombType.disarmable: //wylaczono bombe
                    bombPool.ReturnBombToPool(invokingBomb);
                    break;
                case BombType.nonDisarmable: //bomba wybuchla lub tapnieto czarna
                                             //invokingBomb.SetActive(false);
                    keepBombing = false;

                    endTheGame.Invoke(); //callback do konca gry

                    foreach (GameObject temp in ActiveBombs)
                    {
                        temp.GetComponent<Bomb>().detonationSpeedFactor = 5f; // tutaj mozna dodac ze szybciej bomby wybuchaja czy cos za pomoca detonateSpeedFactor
                    }
                    break;
                case BombType.dontTapExpired: // czarna bomba znika
                    bombPool.ReturnBombToPool(invokingBomb);
                    break;
                default: // jakas bomba wybucha po pregranej - nic nie trzeba robic
                    break;
            }
        }

        void ReduceTimer()
        {
            bombTimerMin = Mathf.Max(Constants.LOWEST_MIN_TIMER, bombTimerMin - 0.01f * bombInterval);
            bombTimerMax = Mathf.Max(Constants.LOWEST_MAX_TIMER, bombTimerMax - 0.01f * bombInterval);
        }
    }
}
