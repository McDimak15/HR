using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok.Compat
{
    public class HorizonCompat : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out var coj))
            {
                if (coj.TryFind("Horizon", out ModItem horizon))
                    return entity.type == horizon.Type;
            }
            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (ModLoader.TryGetMod("SOTS", out var sots))
            {
                if (sots.TryFind("SubspaceBoosters", out ModItem boosters))
                {
                    boosters.UpdateAccessory(player, hideVisual);
                }
            }
        }
    }
}
