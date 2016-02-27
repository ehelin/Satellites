namespace Shared
{
    public class Enumerations
    {
        public enum SatelliteAscentDirection
        {
            East = 0,
            NorthEast = 1,
            North = 2,
            NorthWest = 3,
            West = 4,
            SouthWest = 5,
            South = 6,
            SouthEast = 7,
            NotSet = 8
        }

        public enum UpdateType
        {
            _ClientUpdate_ = 0,
            _StatusUpdate_ = 1
        }
    }
}
