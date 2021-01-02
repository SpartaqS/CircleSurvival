using UnityEngine;
using UnityEngine.UI;

namespace CircleSurvival
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] RectTransform explosionRectTransfrorm = null;
        [SerializeField] Image explosionImage = null;
        Vector3 explosionSpeed = new Vector3(0.5f, 0.5f, 0f);
        float explodeTimer = 0;
        bool startedFadingOut = false;

        private void Update()
        {
            explodeTimer += Time.deltaTime;
            explosionRectTransfrorm.localScale += explosionSpeed * Time.deltaTime / explodeTimer;

            if (explodeTimer > 1.5f && !startedFadingOut)
            {
                explosionImage.CrossFadeAlpha(0f, 0.75f, false);
                startedFadingOut = true;
            }

            if (explodeTimer > 2f)
                gameObject.SetActive(false);
        }
    }
}