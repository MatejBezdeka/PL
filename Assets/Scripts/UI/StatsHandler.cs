using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StatsHandler : MonoBehaviour {
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Slider xpSlider;
    public void ammoChangeAmmoInMag(int ammo, int maxAmmo) { 
        ammoText.text = ammo + "/" + maxAmmo;
    }
    
    public void ammoChangeReaload() {
        ammoText.text = "Reloading";
    }
    public void ammoChangeSwitch() {
        ammoText.text = "Switching";
    }
    public void hpChange(int hp, int maxHp) {
        hpText.text = "Hp:" + hp + "/" + maxHp;
    }
    public void scoreChange(int score) {
        scoreText.text = "Score:" + score;
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