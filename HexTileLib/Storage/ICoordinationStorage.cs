namespace HexTileLib.Storage;

using System;
using Coordinates;

public interface ICoordinationStorage<in TCoordinates, TObject, T> where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
                                                                   where TCoordinates : ICoordinates<T>
{
    public TObject this[TCoordinates coordinates]
    {
        get;
    }
    public void    Add(TCoordinates coordinates, TObject obj);
    public TObject Get(TCoordinates coordinates);
}