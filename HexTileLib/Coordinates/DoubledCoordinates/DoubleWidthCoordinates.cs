namespace HexTileLib.Coordinates.DoubledCoordinates;

using System;
using System.Runtime.CompilerServices;
using Silk.NET.Maths;

public readonly struct DoubleWidthCoordinates<T> : ICoordinates<T> where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
{
    public DoubleWidthCoordinates<T> ToDoubleCoordinates()
    {
        return this;
    }
    public T Row => R;
    public T Col => Q;
    public T Q
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Coords.X;
        }
    }

    public T R
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Coords.Y;
        }
    }
    
    public T S
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Scalar.Subtract(Scalar.Negate(Coords.X), Coords.Y);
        }
    }
    
    public Vector2D<T> Coords
    {
        get;
    }
    
    public DoubleWidthCoordinates(T col, T row)
    {
        Coords = new Vector2D<T>(col, row);
    }
    public static explicit operator Vector2D<T>(DoubleWidthCoordinates<T> left)
    {
        return left.Coords;
    }

    public bool Equals(DoubleWidthCoordinates<T> other)
    {
        return Coords == other.Coords;
    }
    public override string ToString()
    {
        return Coords.ToString();
    }
    public AxialCoordinates<T> ToAxialCoordinates()
    {
        var q = Scalar.Divide(Scalar.Subtract(Col, Row), Scalar<T>.Two);
        var r = Row;
        return new AxialCoordinates<T>(q, r);
    }
    public override bool Equals(object obj)
    {
        return obj is DoubleWidthCoordinates<T> other && Equals(other);
    }
#if NETCOREAPP || NETSTANDARD2_1
    public override int GetHashCode()
    {
        return HashCode.Combine(Q, R);
    }
#elif NETSTANDARD2_0
    public override int GetHashCode()
    {
        return 17 + Q.GetHashCode() * 23 + R.GetHashCode() * 31;
    }
#endif
    public static bool operator ==(DoubleWidthCoordinates<T> left, DoubleWidthCoordinates<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DoubleWidthCoordinates<T> left, DoubleWidthCoordinates<T> right)
    {
        return !left.Equals(right);
    }

    public void Deconstruct(out T q, out T r, out T s)
    {
        q = Q;
        r = R;
        s = S;
    }
}