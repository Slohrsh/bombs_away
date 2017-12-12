namespace bombs_away
{
    public class TiledObjectCodes
    {
        public enum mapCodes
        {
            EMPTY_SPACE = 0,
            GROUND_WITH_GRASS = 44,
            DIRT = 54
        }

        //+60 da tiled bei dem zweiten tileset bei der letzten zahl vom ersten tileset weiterzählt
        public enum characterCodes
        {
            ENEMY_VAMPIRE = 173+60,
            PLAYER = 207+60,
            PORTAL = 23+60
        }
    }
}