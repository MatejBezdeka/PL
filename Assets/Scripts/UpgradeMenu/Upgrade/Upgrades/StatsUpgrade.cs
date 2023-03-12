using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace UpgradeMenu.Upgrade.Upgrades {
    public abstract class StatsUpgrade: Upgrade {

        [SerializeField] protected float value;
        public bool percents { get; protected set; }
        type upgradeType;
        rarity upgradeRarity;
        Random rn = new Random();
        
        public override void SetRarity(){
            int luck = rn.Next(0, 100);
            switch (luck) {
                case >96:
                    upgradeRarity = rarity.legendary;
                    value *= 4;
                    break;
                case >90:
                    upgradeRarity = rarity.epic;
                    value *= 3;
                    break;
                case >83:
                    upgradeRarity = rarity.rare;
                    value *= 2;
                    break;
                default:
                    upgradeRarity = rarity.common;
                    break;
            }
        }

        public override string GetDescription() {
            if (percents) {
                return description + " " + value + "%";
            } 
            return description + " " + value;

        }
        public override void UndoRarityValues() {
            switch (upgradeRarity) {
                case rarity.legendary:
                    value /= 4;
                    break;
                case rarity.epic:
                    value /= 3;
                    break;
                case rarity.rare:
                    value /= 2;
                    break;
                case rarity.common:
                    break;
                default:
                    break;
            }
            upgradeRarity = rarity.none;
        }
        public override type GetType() {
            return upgradeType;
        }

        public override Color GetColor() {
            switch (upgradeRarity) {
                case rarity.common:
                    return Color.gray;
                case rarity.rare:
                    return Color.red;
                case rarity.epic:
                    return Color.magenta;
                case rarity.legendary:
                    return Color.yellow;
                case rarity.special:
                    return Color.cyan;
            }
            return Color.black;
        }
    } 
}