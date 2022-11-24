using System.Collections;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator comboAnimator;
    TMPro.TextMeshProUGUI text;
    // int countForDelay;
    // public void playAddPointsAnimationWithDelay(int comboX)
    // {
    //     switch (countForDelay)
    //     {
    //         case 0:
    //             PlayAddPointsAnimation(comboX);
    //             break;
    //         case 1:
    //             WaitForAnimation(comboX, 0.1f);
    //             break;
    //         case 2:
    //             WaitForAnimation(comboX, 0.2f);
    //             break;
    //         case 3:
    //             WaitForAnimation(comboX, 0.3f);
    //             break;
    //         case 4:
    //             WaitForAnimation(comboX, 0.4f);
    //             break;
    //         case 5:
    //             WaitForAnimation(comboX, 0.5f);
    //             break;
    //         case 6:
    //             WaitForAnimation(comboX, 0.6f);
    //             break;
    //         case 7:
    //             WaitForAnimation(comboX, 0.7f);
    //             break;
    //         case 8:
    //             WaitForAnimation(comboX, 0.8f);
    //             break;
    //         case 9:
    //             WaitForAnimation(comboX, 0.9f);
    //             break;
    //     }
    // }
    public void PlayComboAnimation(int comboX)
    {
        comboAnimator = GetComponent<Animator>();
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "x" + comboX.ToString();
        comboAnimator.Play(StaticData.ComboAnimation);
        StartCoroutine(WaitForOffAnimation());

        // StartCoroutine(WaitForAnimationReset());
    }
    public void PlayAddPointsAnimation(int points)
    {
        comboAnimator = GetComponent<Animator>();
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "+" + points.ToString();
        comboAnimator.Play(StaticData.AddPointsAnimation);
        StartCoroutine(WaitForOffAnimation());
    }
    IEnumerator WaitForOffAnimation()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    IEnumerator WaitForAnimation(int comboX, float second)
    {
        yield return new WaitForSeconds(second);
        PlayAddPointsAnimation(comboX);
    }
    // IEnumerator WaitForAnimationReset()
    // {
    //     yield return new WaitForSeconds(2f);
    //     countForDelay = 0;
    // }
}
