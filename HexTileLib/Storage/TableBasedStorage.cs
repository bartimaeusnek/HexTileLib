using System.Collections.Generic;
using System.Numerics;
using HexTileLib.Coordinates;

namespace HexTileLib.Storage
{
    public class TableBasedStorage<T> where T : ICoordinates
    {
#if NETCOREAPP
          private Dictionary<Vector2, T> _dict = new Dictionary<Vector2, T>();
#elif NETSTANDARD2_0
        private Dictionary<(int X, int Z), T> _dict = new Dictionary<(int X, int Z), T>();
#endif
      

        public T this[int x, int z]
        {
            get => this.Get(x, z);
        }
#if NETCOREAPP
        public void Add(T obj)
            => this._dict.Add(new Vector2(obj.q, obj.s), obj);

        public T Get(int x, int z)
            => this._dict[new Vector2(x, z)];
#elif NETSTANDARD2_0
        public void Add(T obj)
            => this._dict.Add((obj.q, obj.s), obj);

        public T Get(int x, int z)
            => this._dict[(x, z)];
#endif
    }
}