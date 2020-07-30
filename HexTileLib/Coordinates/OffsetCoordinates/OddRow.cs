#if NETCOREAPP
using System.Numerics;
#endif

namespace HexTileLib.Coordinates.OffsetCoordinates
{
    public class OddRow : OffsetCoordinatesBase
    {
        public OddRow(int row, int col) : base(row, col) { }
        
        public static implicit operator CubeCoordinates(OddRow evenColumn) => OffsetToCube(evenColumn);
        public static implicit operator OddRow(CubeCoordinates evenColumn) => CubeToOffset(evenColumn);
        public static implicit operator AxialCoordinates(OddRow evenColumn) => OffsetToCube(evenColumn);
        public static implicit operator OddRow(AxialCoordinates evenColumn) => CubeToOffset(evenColumn);

        public static explicit operator OddRow(EvenRow    evenRow) => (CubeCoordinates) evenRow;
        public static explicit operator OddRow(EvenColumn evenRow) => (CubeCoordinates) evenRow;
        public static explicit operator OddRow(OddColumn     evenRow) => (CubeCoordinates) evenRow;

#if NETCOREAPP
        public static implicit operator Vector3(OddRow left) => (CubeCoordinates) left;
        public static implicit operator OddRow(Vector3 left) => (CubeCoordinates) left;
        public static implicit operator Vector2(OddRow left) => (AxialCoordinates) left;
        public static implicit operator OddRow(Vector2 left) => (AxialCoordinates) left;
#endif
        
        private static CubeCoordinates OffsetToCube(OffsetCoordinatesBase offset)
        {
            var x = offset.Col - (offset.Row - (offset.Row & 1)) / 2;
            var z = offset.Row;
            var y = -x - z;
            return new CubeCoordinates(x, y, z);
        }

        private static OddRow CubeToOffset(CubeCoordinates cube)
        {
            var (x, _, z) = cube;
            var col = x + (z - (z & 1)) / 2;
            return new OddRow(z,col);
        }
    }
}