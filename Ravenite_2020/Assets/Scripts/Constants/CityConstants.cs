namespace Constants
{
    public class Factions
    {
        public enum Faction { Player, Enemy_1, Enemy_2, Enemy_3, Enemy_4, Police, Neutral }
    }

    public class Buildings
    { 
        public enum Flag { Selectable, Unselectable }
    }

    public class Materials
    {
        public enum Asset { Building_Player, Building_Enemy_1, Building_Enemy_2, Building_Enemy_3, Building_Enemy_4, Building_Police, Building_Neutral }

        public static Materials.Asset BuildingToMaterial(Factions.Faction faction)
        {
            if (faction == Factions.Faction.Player)
            {
                return Materials.Asset.Building_Player;
            }
            else if (faction == Factions.Faction.Enemy_1)
            {
                return Materials.Asset.Building_Enemy_1;
            }
            else if (faction == Factions.Faction.Enemy_2)
            {
                return Materials.Asset.Building_Enemy_2;
            }
            else if (faction == Factions.Faction.Enemy_3)
            {
                return Materials.Asset.Building_Enemy_3;
            }
            else if (faction == Factions.Faction.Enemy_4)
            {
                return Materials.Asset.Building_Enemy_4;
            }
            else if (faction == Factions.Faction.Police)
            {
                return Materials.Asset.Building_Police;
            }
            else if (faction == Factions.Faction.Neutral)
            {
                return Materials.Asset.Building_Neutral;
            }
            else
            {
                return 0;
            }
        }
    }
}
