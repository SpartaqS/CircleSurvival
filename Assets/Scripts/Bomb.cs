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

    bool isArmed;

    Action<GameObject,int> bombStatus;

    Bomb()
    {
        isArmed = false;
    }
    // tutaj bedzie krzyczec ze sie wylaczylo bombe !!!
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) 
    {
        bombStatus.Invoke(this.gameObject, 0);
        this.gameObject.SetActive(false); // robocze BombManager to bedzie robic!!!
    }

    public void ReduceDetonateTime(float timeReduct)
    {
        timeToDetonate -= timeReduct;
        TimeDisplay.fillAmount = Mathf.Max(0, timeToDetonate / startingDetonateTime);
        if(timeToDetonate <= 0)
            bombStatus.Invoke(this.gameObject, 1);
    }

    public void CircleReset(float timeToDetonate,Action<GameObject,int> bombStatus)
    {
        this.timeToDetonate = timeToDetonate;
        startingDetonateTime = timeToDetonate;
        this.bombStatus += bombStatus;
    }
}
