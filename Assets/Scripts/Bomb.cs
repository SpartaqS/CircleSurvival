using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bomb : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image TimeDisplay;

    float startingDetonateTime;
    [SerializeField] float timeToDetonate;

    public bool isArmed;
    float detonationSpeedFactor = 1f;

    Action<GameObject,int> bombStatus;

    Bomb()
    {
        isArmed = false;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) 
    {
        bombStatus.Invoke(this.gameObject, 0);
    }

    void Update()
    {
        if (isArmed)
        {
            timeToDetonate -= Time.deltaTime * detonationSpeedFactor;
            TimeDisplay.fillAmount = Mathf.Max(0, timeToDetonate / startingDetonateTime);
            if (timeToDetonate <= 0)
                bombStatus.Invoke(this.gameObject, 1);
        }
    }

    public void CircleReset(float timeToDetonate, Action<GameObject, int> bombStatus)
    {
        Debug.Log("Circle spawned");
        this.timeToDetonate = timeToDetonate;
        startingDetonateTime = timeToDetonate;
        this.bombStatus += bombStatus;
        isArmed = true;
    }
}
