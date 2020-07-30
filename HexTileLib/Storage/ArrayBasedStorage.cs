using HexTileLib.Coordinates;

namespace HexTileLib.Storage
{
    public class ArrayBasedStorage<T> where T : ICoordinates
    {
        private T[] _storage;
        private int _YLength;

        public ArrayBasedStorage(int factor, int height)
        {
            this._storage = new T[factor * height];
            this._YLength = height;
        }
        
        public T this[int x, int z]
        {
            get => this.Get(x, z);
        }

        public void Add(T obj) 
            => this._storage.Set2DCoordTo1DArray(obj.q, obj.s, this._YLength, obj);
        
        public T Get(int x, int z) 
            => this._storage.Get2DCoordFrom1DArray(x, z, this._YLength);
    }
}