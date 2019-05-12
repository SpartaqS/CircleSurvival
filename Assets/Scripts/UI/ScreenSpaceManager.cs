using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceManager : MonoBehaviour
{
    int screenWidth = Screen.width;
    int screenHeight = Screen.height;

    public BombManager bombManager;

    // wartosci referencyjne
    int rScreenWidth = 1080;
    int rScreenHeight = 1920;
    float widthScale, heightScale;
    int rHUDHeight = 400;
    public const int borderDistanceOffset = 96; // promien bomby + 32
    // heightMax to po prostu rScreenHeight

    internal void CalculateGameplayArea()
    {
        bombManager.gameplayWidthMin = (int)(borderDistanceOffset);
        bombManager.gameplayWidthMax = (int)((rScreenWidth - borderDistanceOffset));
        bombManager.gameplayHeightMin = (int)((rHUDHeight + borderDistanceOffset));
        bombManager.gameplayHeightMax = (int)((rScreenHeight - borderDistanceOffset));
    }

}
