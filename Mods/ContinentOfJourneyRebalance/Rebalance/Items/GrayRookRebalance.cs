using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney;

namespace HomewardRagnarok.Compat
{
    public class GrayRookPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<GrayRook>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            base.UpdateAccessory(item, player, hideVisual);

            var modPlayer = player.GetModPlayer<TemplatePlayer>();
            float speedToRemove = modPlayer.MinionCount * 0.02f;

            player.GetAttackSpeed(DamageClass.Melee) -= speedToRemove;
        }

        public override void ModifyTooltips(Item item, System.Collections.Generic.List<TooltipLine> tooltips)
        {
            if (item.type != ModContent.ItemType<GrayRook>())
                return;

            foreach (var line in tooltips)
            {
                if (line.Text.Contains("melee speed"))
                {
                    line.Text = line.Text.Replace("Increase your melee speed by 2% and", "Increase your");
                }
            }
        }
    }
}
