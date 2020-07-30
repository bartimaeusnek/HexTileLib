using System;
using System.Collections.Generic;
using HexTileLib.Coordinates;

namespace HexTileLib.Storage
{
    public class DynamicArrayStorage<T> where T : ICoordinates
    {
        private T[] _storage;
        private Dictionary<ulong, ulong> _lookupTable;
        private ulong _counter = ulong.MinValue;
        private ulong _factor;
        private float _threshold;
        public DynamicArrayStorage(ulong factor = 1000, float threshold = 0.75f)
        {
            this._factor = factor;
            this._threshold = threshold;
            this._storage = new T[factor];
            this._lookupTable = new Dictionary<ulong, ulong>();
        }
        
        internal static void PreventOverflow(ulong ctr)
        {
            if (ctr == ulong.MaxValue)
                throw new OverflowException($"This {nameof(DynamicArrayStorage<T>)} is full!");
        }
        
        public void Add(T obj)
        {
            PreventOverflow(this._counter);
            if (this._counter > (this._storage.Length * this._threshold))
                this._storage = this._storage.ResizeStorage(this._factor);
            var key = (ulong)((uint)obj.q) << 32 |((uint) obj.s);
#if NETCOREAPP
            if (this._lookupTable.TryAdd(key, this._counter))
            {
#elif NETSTANDARD2_0
            if (!this._lookupTable.ContainsKey(key))
            {
                this._lookupTable.Add(key, this._counter);
#endif
                this._storage[this._counter] = obj;
                ++this._counter;
            }
            else
                throw new ArgumentException($"{nameof(obj)}: {obj.ToString()} is already in {nameof(DynamicArrayStorage<T>)}, at Position {key} / {this._lookupTable[key]}");
        }
        
        public T this[uint x, uint z]
        {
            get => this.Get(x,z);
        }

        public T this[int x, int z]
        {
            get => this[(uint)x, (uint) z];
        }
        
        public T Get(int x, int z) => this.Get((uint) x, (uint) z);
        
        public T Get(uint x, uint z)
            => this.Get(this._lookupTable[(ulong)x << 32 | z]);

        private T Get(ulong @internal)
            => this._storage[@internal];

        public void Clear()
        {
            this._lookupTable.Clear();
            this._storage = new T[this._factor];
            this._counter = uint.MinValue;
        }
    }
}