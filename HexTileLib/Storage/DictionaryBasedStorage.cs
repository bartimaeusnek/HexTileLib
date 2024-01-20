namespace HexTileLib.Storage;

using System;
using System.Collections.Generic;
using Coordinates;

public class DictionaryBasedStorage<TCoordinates, TObject, T> : ICoordinationStorage<TCoordinates, TObject, T> where TCoordinates : ICoordinates<T>
                                                                                                               where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
{
    private readonly Dictionary<TCoordinates, TObject> _dict = new Dictionary<TCoordinates, TObject>();

    public void Add(TCoordinates coordinates, TObject obj)
    {
        _dict.Add(coordinates, obj);
    }
    public TObject Get(TCoordinates coordinates)
    {
        return _dict[coordinates];
    }
    public TObject this[TCoordinates coordinates]
    {
        get
        {
            return _dict[coordinates];
        }
        set
        {
            _dict[coordinates] = value;
        }
    }
}