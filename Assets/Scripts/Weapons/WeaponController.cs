using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour {
    public List<Weapon> listOfWeapons { get; private set; }
    int currentWeaponIndex = 0;
    Weapon currentWeapon;
    PlayerController playerController;
    [SerializeField] GameObject inventory;
    PlayerInput playerInput;
    //[SerializeField] public GameObject bullet { get; }
    WeaponState state;
    void Awake() {
        GetWeapons();
        currentWeapon = listOfWeapons[currentWeaponIndex];
        playerController = GetComponent<PlayerController>();
        state = new NormalState(currentWeapon, playerController);
        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void GetWeapons() {
        listOfWeapons = inventory.GetComponentsInChildren<Weapon>().ToList();
    }
    void Update() {
        if (GameManager.manager.currentGameState != GameManager.gameState.go) {
            return;
        }
        if (/*Input.GetButtonDown("Pistol")*/playerInput.actions["Pistol"].triggered) {
            SwitchGuns(listOfWeapons[0]);

        }else if (/*Input.GetButtonDown("SMG")*/playerInput.actions["SMG"].triggered) {
            SwitchGuns(listOfWeapons[1]);

        }else if (/*Input.GetButtonDown("Ar")*/playerInput.actions["AR"].triggered) {
            SwitchGuns(listOfWeapons[2]);

        }else if (/*Input.GetButtonDown("Shotgun")*/playerInput.actions["Shotgun"].triggered) {
            SwitchGuns(listOfWeapons[3]);

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
        Debug.Log("new gun " + newWeapon.name);
        foreach (Weapon weapon in listOfWeapons) {
            if (weapon == newWeapon) {
                newWeapon.available = true;
                return;
            }
        }
    }
}