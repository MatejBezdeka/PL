using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StatsHandler : MonoBehaviour {
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Slider xpSlider;
    [SerializeField] Image gunIcon;
    [SerializeField] Image bloodOnScreen;
   
    public void ammoChangText(string massage) {
        ammoText.text = massage;
    }
    public void hpChange(int hp, int maxHp) {
        hpText.text = "Hp:" + hp + "/" + maxHp;
        if (maxHp > hp) {
            if (hp == 0) {
                bloodOnScreen.color = new Color(255, 255, 255, 255);
            }
            else {
                bloodOnScreen.color = new Color(1, 1, 1, hp/maxHp + 0.01f);
            }
        }
        else {
            bloodOnScreen.color = Color.clear;
        }
    }
    public void scoreChange(int score) {
        scoreText.text = "Score:" + score;
    }

    public void ChangeGunIcon(Sprite gunSprite) {
        gunIcon.sprite = gunSprite;
    }
    public bool xpChange(int xp) {
        int sum = (int)(xpSlider.value += xp);
        if (sum < xpSlider.maxValue) {
            xpSlider.value = sum;
        }
        else {
            sum -= (int)xpSlider.maxValue;
            xpSlider.value = sum;
            xpSlider.maxValue += (int) xpSlider.maxValue / 5;
            return true;
        }
        return false;
    }
}