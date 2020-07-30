#if NETCOREAPP
using System.Numerics;
#endif

namespace HexTileLib.Coordinates.OffsetCoordinates
{
    public class OddColumn : OffsetCoordinatesBase
    {
        public OddColumn(int row, int col) : base(row, col) { }
        
        public static implicit operator CubeCoordinates(OddColumn evenColumn) => OffsetToCube(evenColumn);
        public static implicit operator OddColumn(CubeCoordinates evenColumn) => CubeToOffset(evenColumn);
        public static implicit operator AxialCoordinates(OddColumn evenColumn) => OffsetToCube(evenColumn);
        public static implicit operator OddColumn(AxialCoordinates evenColumn) => CubeToOffset(evenColumn);

        public static explicit operator OddColumn(EvenRow   evenRow) => (CubeCoordinates) evenRow;
        public static explicit operator OddColumn(EvenColumn evenRow) => (CubeCoordinates) evenRow;
        public static explicit operator OddColumn(OddRow    evenRow) => (CubeCoordinates) evenRow;


#if NETCOREAPP
        public static implicit operator Vector3(OddColumn left) => (CubeCoordinates) left;
        public static implicit operator OddColumn(Vector3 left) => (CubeCoordinates) left;
        public static implicit operator Vector2(OddColumn left) => (AxialCoordinates) left;
        public static implicit operator OddColumn(Vector2 left) => (AxialCoordinates) left;
#endif
        
        private static CubeCoordinates OffsetToCube(OffsetCoordinatesBase offset)
        {
            var x = offset.Col;
            var z = offset.Row - (offset.Col - (offset.Col & 1)) / 2;
            var y = -x         - z;
            return new CubeCoordinates(x, y, z);
        }

        private static OddColumn CubeToOffset(CubeCoordinates cube)
        {
            var (x, _, z) = cube;
            var row = z + (x - (x & 1)) / 2;
            return new OddColumn(row,x);
        }
    }
}