using System;
using Terraria;
using Terraria.ModLoader;
using EpicSauceModJam.NPCs;
using Microsoft.Xna.Framework;

namespace EpicSauceModJam.Items.Sauce
{
    public class SauceDrop : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Drop of Sauce");
            Tooltip.SetDefault("The most basic form of sauce");
            GlobalDropNPC.GlobalDrops.Add(new GlobalDrop(GlobalDrop.AlwaysDrop, ModContent.ItemType<SauceDrop>(), GlobalDrop.One)); // Make everything drop one sauce drop with a 100% chance
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.Size = new Vector2(14, 20);
            item.value = Item.sellPrice(copper: 1);
            item.maxStack = 9999;
        }
    }
}
