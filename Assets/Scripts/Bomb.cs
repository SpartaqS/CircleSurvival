using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace CircleSurvival
{
    public enum BombType
    {
        disarmable = 0,
        nonDisarmable = 1,
        dontTapExpired = 2,
    }

    public class Bomb : MonoBehaviour, IPointerDownHandler, IBomb
    {
        [SerializeField] Image TimeDisplay = null;

        float startingDetonateTime;
        [SerializeField] float timeToDetonate;

        public bool isArmed;
        public float detonationSpeedFactor = 1f;

        BombType bombType;

        Action<GameObject, BombType> bombStatus;

        Bomb()
        {
            isArmed = false;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (isArmed)
                bombStatus.Invoke(this.gameObject, bombType);
        }

        void Update()
        {
            if (isArmed)
            {
                timeToDetonate -= Time.deltaTime * detonationSpeedFactor;
                if (bombType == BombType.disarmable)
                    TimeDisplay.fillAmount = Mathf.Max(0, timeToDetonate / startingDetonateTime);
                if (timeToDetonate <= 0)
                {
                    isArmed = false;
                    int invokeOffset = 1;  // bomba do rozbrojenia, ktora wybucha moze byc traktowana tak samo jak czarna bomba ktora tapnieto. czarna bomba ktorej konczy sie czas po prostu znika
                    if (detonationSpeedFactor > 2f)
                        invokeOffset = 4; // bomba wybucha już po przegranej: BombManager nie musi rozpatrywać tej bomby
                    bombStatus.Invoke(this.gameObject, bombType + invokeOffset);
                }
            }
        }

        public void CircleReset(float timeToDetonate, Action<GameObject, BombType> bombStatusManager, BombType bombType, Action<GameObject, BombType> bombStatusExplodeFX)
        {
            this.timeToDetonate = timeToDetonate;
            startingDetonateTime = timeToDetonate;
            this.bombStatus = null;
            this.bombStatus += bombStatusManager;
            this.bombStatus += bombStatusExplodeFX;
            this.bombType = bombType;

            TimeDisplay.fillAmount = 1;
            if (bombType == BombType.disarmable)
                TimeDisplay.color = Color.green;
            else // bombType = BombType.nonDisarmable
                TimeDisplay.color = Color.black;

            isArmed = true;
        }
    }
}
