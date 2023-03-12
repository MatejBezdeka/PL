using UnityEngine;
namespace UpgradeMenu.Upgrade {
    public abstract class Upgrade : MonoBehaviour {
        [SerializeField] Sprite picture;
        public Sprite Picture => picture;
        [SerializeField] protected string text;
        [SerializeField] protected string description;
        public string upgradeText => text;
        public string Description => description;
        //protected Color color;
        // barva rarity
        public enum rarity {
            none ,common, rare, epic, legendary, special
        }
        public enum type {
            none ,weaponUp, playerUp, heal, weapon, other
        }

        void Start() {
            
            /*if (upgradeType == type.weapon) {
                color = Color.green;
            }*/
            
        }
        public abstract void UndoRarityValues();
        public abstract string GetDescription();
        public abstract void SetRarity();
        public abstract type GetType();
        public abstract Color GetColor();
        public abstract void Use(PlayerController playerController, WeaponController weaponController);
    }
}