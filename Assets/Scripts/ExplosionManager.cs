using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] Canvas spawningCanvas = null;
    [SerializeField] GameObject ExplosionEffect = null;
    public void DoExplosionEffect(GameObject invokingBomb, int messageID)
    {
        if (messageID == 1 || messageID > 3)
        {
            GameObject explosion = Instantiate(ExplosionEffect, spawningCanvas.transform);
            explosion.gameObject.transform.SetAsLastSibling();
            explosion.GetComponent<RectTransform>().anchoredPosition = invokingBomb.GetComponent<RectTransform>().anchoredPosition;
            invokingBomb.SetActive(false);
        }
        if (messageID == 0)
        {
            GetComponent<AudioSource>().pitch = 1f;
            GetComponent<AudioSource>().Play();
            invokingBomb.SetActive(false);
        }
        if (messageID == 2)
        {
            GetComponent<AudioSource>().pitch = 0.8f;
            GetComponent<AudioSource>().Play();
            invokingBomb.SetActive(false);
        }
    }
}
