using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EpicSauceModJam.NPCs
{
    /// <summary>
    /// Allows you to wrap a drop and it's condition in a single type
    /// </summary>
    public struct GlobalDrop
    {
        public static DropCondition AlwaysDrop = (npc) => true;
        public static DropCondition PrecentChance(float chance) => (npc) => Main.rand.NextFloat(chance) <= chance;
        public static Tuple<int, int> One => new Tuple<int, int>(1, 1);

        public delegate bool DropCondition(NPC npc);
        public DropCondition Predicate;
        public int Drop;
        public Tuple<int, int> Range;

        public GlobalDrop(DropCondition predicate, int drop, Tuple<int, int> range)
        {
            Predicate = predicate;
            Drop = drop;
            Range = range;
        }
        
        public bool TryDrop(NPC npc)
        {
            // Don't drop if it shouldn't drop
            if (!Predicate(npc))
                return false;

            Item.NewItem(npc.getRect(), Drop, Main.rand.Next(Range.Item1, Range.Item2 + 1));

            return true;
        }
    }

    /// <summary>
    /// You can add entries to GlobalDrops if you don't want to hardcode your globalnpc dropping code
    /// </summary>
    public class GlobalDropNPC : GlobalNPC
    {
        public static List<GlobalDrop> GlobalDrops = new List<GlobalDrop>();

        public override void NPCLoot(NPC npc)
        {
            base.NPCLoot(npc);
            
            foreach (GlobalDrop globalDrop in GlobalDrops)
            {
                globalDrop.TryDrop(npc);
            }
        }
    }
}
