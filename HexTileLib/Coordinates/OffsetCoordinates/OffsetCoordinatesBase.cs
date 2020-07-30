#if NETCOREAPP
using System.Numerics;
using System.Text.Json.Serialization;
#endif
namespace HexTileLib.Coordinates.OffsetCoordinates
{
    public abstract class OffsetCoordinatesBase : ICoordinates
    {
#if NETCOREAPP
        private Vector2 _vector2;
#elif NETSTANDARD2_0
        private (int X, int Y) _vector2;
#endif
            
        public int Row { get => (int) this._vector2.X; }
        public int Col { get => (int) this._vector2.Y; }
        protected OffsetCoordinatesBase(int row, int col)
        {
#if NETCOREAPP
                this._vector2 = new Vector2(row, col);
#elif NETSTANDARD2_0
                this._vector2 = (row, col);
#endif
        }
        
#if NETCOREAPP
        [JsonIgnore]
#endif
        public int q { get => this.Row; }
#if NETCOREAPP
        [JsonIgnore]
#endif
        public int s { get => this.Col; }
    }
}