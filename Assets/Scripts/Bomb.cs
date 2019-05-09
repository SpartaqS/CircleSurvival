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

    Bomb()
    {
        isArmed = false;
    }
    // tutaj bedzie krzyczec ze sie wylaczylo bombe !!!
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("I AM CIRCLE");
    }

    public void ReduceDetonateTime(float timeReduct)
    {
        timeToDetonate -= timeReduct;
        TimeDisplay.fillAmount = Mathf.Max(0, timeToDetonate / startingDetonateTime);
    }

    public void CircleReset(float timeToDetonate)
    {
        this.timeToDetonate = timeToDetonate;
        startingDetonateTime = timeToDetonate;
    }
}
