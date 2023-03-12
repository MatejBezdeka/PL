using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class WeaponController : MonoBehaviour {
    public List<Weapon> listOfWeapons { get; private set; }
    int currentWeaponIndex = 0;
    Weapon currentWeapon;
    Weapon nextWeapon;
    PlayerController playerController;
    [SerializeField] GameObject inventory;
    //[SerializeField] public GameObject bullet { get; }
    WeaponState state;
    void Awake() {
        GetWeapons();
        currentWeapon = listOfWeapons[currentWeaponIndex];
        playerController = GetComponent<PlayerController>();
        state = new NormalState(currentWeapon, playerController);
    }

    void GetWeapons() {
        listOfWeapons = inventory.GetComponentsInChildren<Weapon>().ToList();
    }
    void Update() {
        if (GameManager.manager.currentGameState != GameManager.gameState.go) {
            return;
        }
        switch (Input.inputString) {
            case "+":
            case "1":
                //pistol
                SwitchGuns(listOfWeapons[0]);
                break;
            case "ě":
            case "2":
                //smg
                SwitchGuns(listOfWeapons[1]);
                break;
            case "š":
            case "3":
                //Ar
                SwitchGuns(listOfWeapons[2]);
                break;
            case "č":
            case "4":
                //shotgun
                SwitchGuns(listOfWeapons[3]);
                break;
        }
        state = state.Process();
    }

    void SwitchGuns(Weapon nextWeapon) {
        if (currentWeapon != nextWeapon && state.name != WeaponState.currentState.switchingGun && nextWeapon.available) {
            state = new SwitchingGunState(currentWeapon, nextWeapon, playerController);
            currentWeapon = nextWeapon;
        }
    }

    public void GetNewWeapon(Weapon newWeapon) {
        foreach (Weapon weapon in listOfWeapons) {
            if (weapon == newWeapon) {
                newWeapon.available = true;
            }
        }
    }
}