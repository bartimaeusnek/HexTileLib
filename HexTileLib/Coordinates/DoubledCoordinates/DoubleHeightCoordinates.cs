namespace HexTileLib.Coordinates.DoubledCoordinates;

using System;
using System.Runtime.CompilerServices;
using Silk.NET.Maths;

public readonly struct DoubleHeightCoordinates<T> : ICoordinates<T> where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
{
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
    public override string ToString()
    {
        return Coords.ToString();
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
    
    public DoubleHeightCoordinates(T row, T col)
    {
        Coords = new Vector2D<T>(col, row);
    }
    public static explicit operator Vector2D<T>(DoubleHeightCoordinates<T> left)
    {
        return left.Coords;
    }

    public bool Equals(DoubleHeightCoordinates<T> other)
    {
        return Coords == other.Coords;
    }

    public AxialCoordinates<T> ToAxialCoordinates()
    {
        var q = Col;
        var r = Scalar.Divide(Scalar.Subtract(Row, Col), Scalar<T>.Two);
        return new AxialCoordinates<T>(q, r);
    }
    public DoubleHeightCoordinates<T> ToDoubleCoordinates()
    {
        return this;
    }
    public override bool Equals(object obj)
    {
        return obj is DoubleHeightCoordinates<T> other && Equals(other);
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
    public static bool operator ==(DoubleHeightCoordinates<T> left, DoubleHeightCoordinates<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DoubleHeightCoordinates<T> left, DoubleHeightCoordinates<T> right)
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