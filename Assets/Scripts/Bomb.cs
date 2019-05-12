using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Bomb : MonoBehaviour, IPointerDownHandler , IBomb
{
    [SerializeField] Image TimeDisplay = null;

    float startingDetonateTime;
    [SerializeField] float timeToDetonate;

    public bool isArmed;
    public float detonationSpeedFactor = 1f;

    int bombType; // 0 - zielona , 1 - czarna donttap

    Action<GameObject,int> bombStatus;

    Bomb()
    {
        isArmed = false;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) 
    {
        if(isArmed)
            bombStatus.Invoke(this.gameObject, bombType);
    }

    void Update()
    {
        if (isArmed)
        {
            timeToDetonate -= Time.deltaTime * detonationSpeedFactor;
            if(bombType == 0)
                TimeDisplay.fillAmount = Mathf.Max(0, timeToDetonate / startingDetonateTime);
            if (timeToDetonate <= 0)
            {
                isArmed = false;
                int invokeOffset = 1;
                if (detonationSpeedFactor > 2f)
                    invokeOffset = 4;
                bombStatus.Invoke(this.gameObject, bombType + invokeOffset);
            }
        }
    }

    public void CircleReset(float timeToDetonate, Action<GameObject, int> bombStatusManager, int bombType, Action<GameObject, int> bombStatusExplodeFX)
    {
        Debug.Log("Circle spawned");
        this.timeToDetonate = timeToDetonate;
        startingDetonateTime = timeToDetonate;
        this.bombStatus = null;
        this.bombStatus += bombStatusManager;
        this.bombStatus += bombStatusExplodeFX;
        this.bombType = bombType;

        TimeDisplay.fillAmount = 1;
        if (bombType == 0)
            TimeDisplay.color = Color.green;
        else
            TimeDisplay.color = Color.black;

        isArmed = true;
    }
}
