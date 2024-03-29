﻿namespace HexTileLib.Coordinates;

using System;
using System.Runtime.CompilerServices;
using DoubledCoordinates;
using Silk.NET.Maths;

public readonly struct AxialCoordinates<T>(T q, T r) : IEquatable<AxialCoordinates<T>>, ICoordinates<T>
    where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
{
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
    } = new Vector2D<T>(q, r);

    public static explicit operator Vector2D<T>(AxialCoordinates<T> left)
    {
        return left.Coords;
    }
    public override string ToString()
    {
        return Coords.ToString();
    }
    public bool Equals(AxialCoordinates<T> other)
    {
        return Coords == other.Coords;
    }

    public AxialCoordinates<T> ToAxialCoordinates()
    {
        return this;
    }
    public DoubleHeightCoordinates<T> ToDoubleCoordinates()
    {
        var col = Q;
        var row = Scalar.Add(Scalar.Multiply(Scalar<T>.Two, R), Q);
        return new DoubleHeightCoordinates<T>(row, col);
    }
    public override bool Equals(object obj)
    {
        return obj is AxialCoordinates<T> other && Equals(other);
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
    public static bool operator ==(AxialCoordinates<T> left, AxialCoordinates<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(AxialCoordinates<T> left, AxialCoordinates<T> right)
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