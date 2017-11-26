using System.Collections.Generic;
using System.Drawing;

namespace LegendsViewer.Legends
{
    public class Location : IEqualityComparer<Location>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public static bool operator ==(Location a, Location b)
        {
            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Location a, Location b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Location b = obj as Location;
            if ((object)b == null)
            {
                return false;
            }

            return X == b.X && Y == b.Y;
        }

        public override int  GetHashCode()
        {
            return X ^ Y;
        }

        public bool Equals(Location b)
        {
            return X == b.X && Y == b.Y;
        }

        public bool Equals(Location a, Location b)
        {
            return a == b;
        }

        public int GetHashCode(Location location)
        {
            return location.X ^ location.Y;
        }
    }
}