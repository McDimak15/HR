using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.LoreItems;
using ContinentOfJourney.Items.Placables;

namespace HomewardRagnarok.Items.LoreItems
{
    public class LoreGoblinChariot : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<ChariotTrophy>().AddTile(101).Register();
        }
    }

    public class LoreBigDipper : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<BigDipperTrophy>().AddTile(101).Register();
        }
    }

    public class LorePuppetOpera : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<PuppetTrophy>().AddTile(101).Register();
        }
    }

    public class LoreMarquisMoonsquid : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<MarquisMoonsquidTrophy>().AddTile(101).Register();
        }
    }

    public class LorePriestessRod : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<PriestessRodTrophy>().AddTile(101).Register();
        }
    }

    public class LoreDiver : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<DiverTrophy>().AddTile(101).Register();
        }
    }

    public class LoreTheMotherbrain : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<TheMotherbrainTrophy>().AddTile(101).Register();
        }
    }

    public class LoreWallofShadow : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<WallOfShadowTrophy>().AddTile(101).Register();
        }
    }

    public class LoreSlimeGod : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<SlimeGodTrophy>().AddTile(101).Register();
        }
    }

    public class LoreOverwatcher : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<TheOverwatcherTrophy>().AddTile(101).Register();
        }
    }
    public class LoreTheMaterealizer : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<TheMaterealizerTrophy>().AddTile(101).Register();
        }
    }
    public class LoreTheLifebringer : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<TheLifebringerTrophy>().AddTile(101).Register();
        }
    }
    public class LoreScarabBelief : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<ScarabBeliefTrophy>().AddTile(101).Register();
        }
    }
    public class LoreWhale : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<EverlastingFallingWhaleTrophy>().AddTile(101).Register();
        }
    }
    public class LoreTheSon : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient<TheSonTrophy>().AddTile(101).Register();
        }
    }
}