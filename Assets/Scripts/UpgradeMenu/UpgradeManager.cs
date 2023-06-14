using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UpgradeMenu.Upgrade;
using Random = System.Random;
public class UpgradeManager : MonoBehaviour {
    Random rn = new Random();
    [SerializeField] PlayerController playerController;
    [SerializeField] WeaponController weaponController;
    [SerializeField] List<TextMeshProUGUI> listOfTexts = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> listOfDescriptions = new List<TextMeshProUGUI>();
    [SerializeField] List<Image> listOfImageBoxes = new List<Image>();
    [SerializeField] List<Button> listOfButtons = new List<Button>();
    [SerializeField] List<Upgrade> listOfUpgrades = new List<Upgrade>();
    [SerializeField] List<Image> listOfPanels = new List<Image>();
    [SerializeField] GameObject upgrades;
    [SerializeField] AudioClip levelUpSound;
    [SerializeField] AudioClip buttonClick;
    float delay = 1.2f;
    float currentdelay = 0;
    List<int> upgradeActive = new List<int>();
    WeaponUpgrade wea;
    void Start() {
        listOfUpgrades = upgrades.GetComponents<Upgrade>().ToList();
        for (int i = 0; i < listOfButtons.Count; i++) {
            // click(i) bylo vždy 3
            int idOfButton = i;
            listOfButtons[i].onClick.AddListener(delegate { Click(idOfButton); });
        }
    }

    void Update() {
        if (currentdelay > 0) {
            currentdelay -= Time.fixedTime;
        }
        else {
            foreach (var button in listOfButtons) {
                button.interactable = true;
            }
        }
    }

    public void LevelUp() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.manager.PlayAudioCLip(levelUpSound);
        for (int i = 0; i < listOfButtons.Count;) {
            listOfButtons[i].interactable = false;
            currentdelay = delay;
            int id = rn.Next(0, listOfUpgrades.Count);
            if (ControlDuplicityOfUpgrade(id)) {
                //Debug.Log("ID: " + id);
                upgradeActive.Add(id);
                listOfUpgrades[id].SetRarity();
                listOfDescriptions[i].text = "";
                if (listOfUpgrades[id].GetType() == Upgrade.type.weaponUp) {
                    wea = (WeaponUpgrade) listOfUpgrades[id];
                    listOfDescriptions[i].text = wea.GetRandomWeaponInInventory(weaponController);
                }
                listOfTexts[i].text = listOfUpgrades[id].upgradeText;
                listOfDescriptions[i].text += listOfUpgrades[id].GetDescription();
                listOfPanels[i].color = listOfUpgrades[id].GetColor();
                listOfImageBoxes[i].sprite = listOfUpgrades[id].Picture;
                i++;
            }
        }
    }
    bool ControlDuplicityOfUpgrade(int upgradeId) {
        foreach (int id in upgradeActive) {
            if (upgradeId == id) {
                return false;
            }
        }
        return true;
    }
    void Click(int button) {
        GameManager.manager.PlayAudioCLip(buttonClick);
        Upgrade upgrade = listOfUpgrades[upgradeActive[button]];
        upgrade.Use(playerController, weaponController);
        foreach (int id in upgradeActive) {
            listOfUpgrades[id].UndoRarityValues();
        }
        if (upgrade.GetType() == Upgrade.type.weapon) {
            listOfUpgrades.Remove(upgrade);
        }
        GameManager.manager.Upgrade();
        upgradeActive.Clear();
    }
}