namespace HexTileLib.Coordinates;

using System;

public interface ICoordinates<out T> where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
{
    public T Q { get; }
    public T R { get; }
}