using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite threeQuarterHeart;
    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite quarterHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] FloatValue heartContainers;
    [SerializeField] FloatValue playerCurrentHealth;

    private void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for(int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }

        updateHearts();
    }

    public void updateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue / 4;
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            float currHeart = Mathf.Ceil(tempHealth - 1);
            if (i <= tempHealth - 1)
            {
                //FullHeart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                //emptyHeart
                hearts[i].sprite = emptyHeart;
            }
            else if (i == currHeart && (tempHealth % 1) == .50)
            {
                //Half full heart
                hearts[i].sprite = halfHeart;
            }
            else if (i == currHeart && (tempHealth % 1) == .25)
            {
                //1/4 heart
                hearts[i].sprite = quarterHeart;
            }
            else /*(i == currHeart && (tempHealth % 1) == .75)*/
            {
                //3/4 heart
                hearts[i].sprite = threeQuarterHeart;
            }
        }
    }
}
