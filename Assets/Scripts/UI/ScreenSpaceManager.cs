using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceManager : MonoBehaviour
{
    int screenWidth = Screen.width;
    int screenHeight = Screen.height;

    [SerializeField] BombManager bombManager;

    // wartosci referencyjne
    int rScreenWidth = 1080;
    int rScreenHeight = 1920;
    float widthScale, heightScale;
    int rHUDHeight = 400;
    public const int borderDistanceOffsetSmall = 32;
    public const int borderDistanceOffsetBig = 160; // OffsetSmall + 2*64 - promień bomb
    // heightMax to po prostu rScreenHeight

    void Start()
    {
        bombManager.gameplayWidthMin = (int)(borderDistanceOffsetSmall);
        bombManager.gameplayWidthMax = (int)((rScreenWidth - borderDistanceOffsetBig));
        bombManager.gameplayHeightMin = (int)((rHUDHeight + borderDistanceOffsetSmall));
        bombManager.gameplayHeightMax = (int)((rScreenHeight - borderDistanceOffsetBig));
        
        bombManager.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}
