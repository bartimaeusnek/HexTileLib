#if NETCOREAPP
using System.Numerics;
#endif
namespace HexTileLib.Coordinates.OffsetCoordinates
{
    public class EvenColumn : OffsetCoordinatesBase
    {
        public EvenColumn(int row, int col) : base(row, col) { }
        
        public static implicit operator CubeCoordinates(EvenColumn evenColumn) => OffsetToCube(evenColumn);
        public static implicit operator EvenColumn(CubeCoordinates evenColumn) => CubeToOffset(evenColumn);
        public static implicit operator AxialCoordinates(EvenColumn evenColumn) => OffsetToCube(evenColumn);
        public static implicit operator EvenColumn(AxialCoordinates evenColumn) => CubeToOffset(evenColumn);

        public static explicit operator EvenColumn(EvenRow evenRow) => (CubeCoordinates) evenRow;
        public static explicit operator EvenColumn(OddColumn  evenRow) => (CubeCoordinates) evenRow;
        public static explicit operator EvenColumn(OddRow     evenRow) => (CubeCoordinates) evenRow;

#if NETCOREAPP
        public static implicit operator Vector3(EvenColumn left) => (CubeCoordinates) left;
        public static implicit operator EvenColumn(Vector3 left) => (CubeCoordinates) left;
        public static implicit operator Vector2(EvenColumn left) => (AxialCoordinates) left;
        public static implicit operator EvenColumn(Vector2 left) => (AxialCoordinates) left;
#endif
        private static CubeCoordinates OffsetToCube(OffsetCoordinatesBase offset)
        {
            var x = offset.Col;
            var z = offset.Row - (offset.Col + (offset.Col & 1)) / 2;
            var y = -x - z;
            return new CubeCoordinates(x, y, z);
        }

        private static EvenColumn CubeToOffset(CubeCoordinates cube)
        {
            var (x, _, z) = cube;
            var row = z + (x + (x & 1)) / 2;
            return new EvenColumn(row,x);
        }
    }
}