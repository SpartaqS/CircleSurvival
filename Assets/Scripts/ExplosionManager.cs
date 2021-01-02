using UnityEngine;

namespace CircleSurvival
{
    public class ExplosionManager : MonoBehaviour
    {
        [SerializeField] Canvas spawningCanvas = null;
        [SerializeField] GameObject explosionEffect = null;

        AudioSource audioSource = null;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void DoExplosionEffect(GameObject invokingBomb, BombType messageID)
        {
            if (messageID == BombType.nonDisarmable || messageID > BombType.dontTapExpired) // bomba powinna wybuchnac (skonczyl sie czas / czarna zostala tapnieta)
            {
                GameObject explosion = Instantiate(explosionEffect, spawningCanvas.transform);
                explosion.gameObject.transform.SetAsLastSibling();
                explosion.GetComponent<RectTransform>().anchoredPosition = invokingBomb.GetComponent<RectTransform>().anchoredPosition;
                invokingBomb.SetActive(false);
            }
            if (messageID == BombType.disarmable) // wylaczono bombe
            {
                audioSource.pitch = 1f;
                audioSource.Play();
                invokingBomb.SetActive(false);
            }
            if (messageID == BombType.dontTapExpired) // czarna bomba zniknela
            {
                audioSource.pitch = 0.8f;
                audioSource.Play();
                invokingBomb.SetActive(false);
            }
        }
    }
}