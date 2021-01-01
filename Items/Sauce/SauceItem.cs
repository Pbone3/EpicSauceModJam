using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace EpicSauceModJam.Items.Sauce
{
    /// <summary>
    /// Handles damage and crit calculations for sauce weapons.
    /// In derived classes, call the base of any method overridden here unless you know what you're doing.
    /// </summary>
    public abstract class SauceItem : ModItem
    {
        public override bool CloneNewInstances => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.summon = false;
            item.thrown = false;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            // Apply damage bonuses
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
            SaucePlayer saucePlayer = player.GetModPlayer<SaucePlayer>();
            add += saucePlayer.sauceDamage;
            mult *= saucePlayer.sauceMult;
            flat += saucePlayer.sauceFlat;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            // Apply crit bonuses
            base.GetWeaponCrit(player, ref crit);
            SaucePlayer saucePlayer = player.GetModPlayer<SaucePlayer>();
            crit += saucePlayer.sauceCrit;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            TooltipLine damageLine = tooltips.FirstOrDefault(tt => tt.Name == "Damage" && tt.mod == "Terraria"); // Find the line that displays damange
            if (damageLine != null) // Make sure we found it
            {
                string[] splitText = damageLine.text.Split(' '); // Should return an array along the lines of { "10", "damage" }
                string damage = splitText.First(); // The amount of damage the weapon deals
                string damageWord = splitText.Last(); // The localized word for damage
                damageLine.text = string.Concat(damage, "saucy", damageWord);
            }
        }
    }
}
