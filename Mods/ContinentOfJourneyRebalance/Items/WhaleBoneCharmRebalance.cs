using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.NPCs.Boss_WorldsEndEverlastingFallingWhale;
using ContinentOfJourney.NPCs.Boss_TheOverwatcher;

namespace HomewardRagnarok
{
    public class WhaleBoneCharmRebalance : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<WhaleBoneCharm>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            bool bossIsAlive = NPC.AnyNPCs(ModContent.NPCType<WorldsEndEverlastingFallingWhale>()) ||
                              NPC.AnyNPCs(ModContent.NPCType<TheOverwatcher>());

            if (!bossIsAlive)
            {
                if (player.TryGetModPlayer(out TemplatePlayer cojPlayer))
                {
                    cojPlayer.WhaleBoneCharm = false;
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "WhaleBoneCharmRebalance", "Effect only active while fighting the Falling Whale or The Overwatcher"));
        }
    }
}