using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace HomewardRagnarok.Compat
{
    public class CommandersGauntletPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return false;

            if (coj.TryFind("CommandersGaunlet", out ModItem gauntlet))
            {
                return entity.type == gauntlet.Type;
            }
            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Melee) -= 0.15f; 
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                TooltipLine line = tooltips[i];
                if (line.Text.Contains("melee speed"))
                {
                    line.Text = line.Text.Replace("melee speed", "whip speed");
                }
            }
        }
    }
}
