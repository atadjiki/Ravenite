namespace Constants
{
    public class Buildings
    {
        public enum Type { Player, Enemy, Police, Neutral }
        public enum Flag { Selectable, Unselectable }
    }

    public class Materials
    {
        public enum Asset { Building_Player, Building_Enemy, Building_Police, Building_Neutral, Building_Selected }

        public static Materials.Asset BuildingToMaterial(Buildings.Type type)
        {
            if (type == Buildings.Type.Player)
            {
                return Materials.Asset.Building_Player;
            }
            else if (type == Buildings.Type.Enemy)
            {
                return Materials.Asset.Building_Enemy;
            }
            else if (type == Buildings.Type.Police)
            {
                return Materials.Asset.Building_Police;
            }
            else if (type == Buildings.Type.Neutral)
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
