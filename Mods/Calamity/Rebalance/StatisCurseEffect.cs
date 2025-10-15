using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    public class StatisCurseBonusPlayer : ModPlayer
    {
        public override void UpdateEquips()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) &&
                calamityMod.TryFind("StatisCurse", out ModItem statisCurse))
            {
                for (int i = 3; i < 10 + Player.extraAccessorySlots; i++)
                {
                    Item item = Player.armor[i];
                    if (item.type == statisCurse.Type)
                    {
                        Player.maxTurrets += 1;

                        if (calamityMod.TryFind<ModBuff>("StatisCurseBuff", out ModBuff statisCurseBuff))
                        {
                            Player.AddBuff(statisCurseBuff.Type, 2);
                        }
                    }
                }
            }
        }
    }

    public class StatisCurseTooltipFix : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if (calamityMod.TryFind("StatisCurse", out ModItem statisCurse))
                {
                    return entity.type == statisCurse.Type;
                }
            }
            return false;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color purple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(purple, white, (float)(0.5 * (1 + System.Math.Sin(timer * 6.2831))));

            int constructionPDAType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.ConstructionPDA>();

            string iconTag = $"[i:{constructionPDAType}] ";

            TooltipLine customLine = new TooltipLine(Mod, "HomewardRagnarokBonus", iconTag + "+1 max sentries (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            };

            tooltips.RemoveAll(t => t.Name == "HomewardRagnarokBonus");

            tooltips.Add(customLine);
        }
    }
}
