using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ContinentOfJourney;
using ContinentOfJourney.Buffs;

namespace HomewardRagnarok.CrossMod
{
    public class MiniLinkaCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                string[] calamityItems = { "AsgardsValor", "AsgardianAegis" };
                foreach (string name in calamityItems)
                {
                    if (calamity.TryFind(name, out ModItem item) && entity.type == item.Type)
                        return true;
                }
            }

            if (ModLoader.TryGetMod("Clamity", out Mod clamity))
            {
                string[] clamityItems = { "SupremeBarrier" };
                foreach (string name in clamityItems)
                {
                    if (clamity.TryFind(name, out ModItem item) && entity.type == item.Type)
                        return true;
                }
            }

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                string[] fargoItems = { "ColossusSoul", "DimensionSoul", "EternitySoul" };
                foreach (string name in fargoItems)
                {
                    if (fargo.TryFind(name, out ModItem item) && entity.type == item.Type)
                        return true;
                }
            }

            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.MiniLinka = true;
            player.AddBuff(ModContent.BuffType<MiniLinkaBuff>(), 2);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                string[] hiddenTooltipItems = { "DimensionSoul", "EternitySoul" };
                foreach (string name in hiddenTooltipItems)
                {
                    if (fargo.TryFind(name, out ModItem hiddenItem) && item.type == hiddenItem.Type)
                    {
                        tooltips.RemoveAll(t => t.Name == "MiniLinkaEffect");
                        return;
                    }
                }
            }
            if (ModLoader.TryGetMod("Clamity", out Mod clamity))
            {
                string[] hiddenTooltipItems2 = {"SupremeBarrier" };
                foreach (string name in hiddenTooltipItems2)
                {
                    if (clamity.TryFind(name, out ModItem hiddenItem) && item.type == hiddenItem.Type)
                    {
                        tooltips.RemoveAll(t => t.Name == "MiniLinkaEffect");
                        return;
                    }
                }
            }

            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color purple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(purple, white, 0.5f * (1f + (float)Math.Sin(timer * MathHelper.TwoPi)));

            int circuitChipType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.CircuitChip>();
            string iconTag = $"[i:{circuitChipType}] ";

            TooltipLine customLine = new TooltipLine(Mod, "MiniLinkaEffect",
                iconTag + "Summons a mini linka to fight for you (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            };

            tooltips.RemoveAll(t => t.Name == "MiniLinkaEffect");
            tooltips.Add(customLine);
        }
    }
}
