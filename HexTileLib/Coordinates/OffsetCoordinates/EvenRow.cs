#if NETCOREAPP
using System.Numerics;
#endif

namespace HexTileLib.Coordinates.OffsetCoordinates
{
    public class EvenRow : OffsetCoordinatesBase
    {
        public EvenRow(int row, int col) : base(row, col) { }

        public static implicit operator CubeCoordinates(EvenRow evenColumn) => OffsetToCube(evenColumn);
        public static implicit operator EvenRow(CubeCoordinates evenColumn) => CubeToOffset(evenColumn);
        public static implicit operator AxialCoordinates(EvenRow evenColumn) => OffsetToCube(evenColumn);
        public static implicit operator EvenRow(AxialCoordinates evenColumn) => CubeToOffset(evenColumn);
        
        public static explicit operator EvenRow(EvenColumn evenRow) => (CubeCoordinates) evenRow;
        public static explicit operator EvenRow(OddColumn evenRow) => (CubeCoordinates) evenRow;
        public static explicit operator EvenRow(OddRow evenRow) => (CubeCoordinates) evenRow;
#if NETCOREAPP
        public static implicit operator Vector3(EvenRow left) => (CubeCoordinates) left;
        public static implicit operator EvenRow(Vector3 left) => (CubeCoordinates) left;
        public static implicit operator Vector2(EvenRow left) => (AxialCoordinates) left;
        public static implicit operator EvenRow(Vector2 left) => (AxialCoordinates) left;
#endif
        
        private static CubeCoordinates OffsetToCube(OffsetCoordinatesBase offset)
        {
            var x = offset.Col - (offset.Row + (offset.Row & 1)) / 2;
            var z = offset.Row;
            var y = -x - z;
            return new CubeCoordinates(x, y, z);
        }

        private static EvenRow CubeToOffset(CubeCoordinates cube)
        {
            var (x, _, z) = cube;
            var col = x + (z + (z & 1)) / 2;
            return new EvenRow(z, col);
        }
    }
}