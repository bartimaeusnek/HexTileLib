namespace HexTileLib.Coordinates.OffsetCoordinates;

using System;
using Silk.NET.Maths;

public readonly struct OddFlat<T>(T row, T col) : ICoordinates<T>, IEquatable<OddFlat<T>> where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
{
#region Base
    private readonly Vector2D<T> _vector2 = new Vector2D<T>(row, col);

    public T Row
    {
        get
        {
            return _vector2.X;
        }
    }
    public T Col
    {
        get
        {
            return _vector2.Y;
        }
    }
    
    public T R
    {
        get
        {
            return Row;
        }
    }

    public T Q
    {
        get
        {
            return Col;
        }
    }
    public bool Equals(OddFlat<T> other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return _vector2.Equals(other._vector2);
    }
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;
        return Equals((OddFlat<T>)obj);
    }
    public override int GetHashCode()
    {
        return _vector2.GetHashCode();
    }
    public static bool operator ==(OddFlat<T> left, OddFlat<T> right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(OddFlat<T> left, OddFlat<T> right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return _vector2.ToString();
    }
#endregion
    
    public static explicit operator CubeCoordinates<T>(OddFlat<T> OddFlat)
    {
        return OffsetToCube(OddFlat);
    }
    public static explicit operator OddFlat<T>(CubeCoordinates<T> evenColumn)
    {
        return CubeToOffset(evenColumn);
    }
    public static explicit operator AxialCoordinates<T>(OddFlat<T> OddFlat)
    {
        return (AxialCoordinates<T>)OffsetToCube(OddFlat);
    }
    public static explicit operator OddFlat<T>(AxialCoordinates<T> evenColumn)
    {
        return CubeToOffset((CubeCoordinates<T>)evenColumn);
    }

    public static explicit operator OddFlat<T>(EvenPointy<T> evenPointy)
    {
        return (OddFlat<T>)(CubeCoordinates<T>)evenPointy;
    }
    public static explicit operator OddFlat<T>(EvenFlat<T> evenRow)
    {
        return (OddFlat<T>)(CubeCoordinates<T>)evenRow;
    }
    public static explicit operator OddFlat<T>(OddPointy<T> evenPointy)
    {
        return (OddFlat<T>)(CubeCoordinates<T>)evenPointy;
    }

    private static OddFlat<T> CubeToOffset(CubeCoordinates<T> cube)
    {
        var (q, r, _) = cube;
        var col = q;
        var row = Scalar.Add(r, Scalar.Divide(Scalar.Subtract(q, Scalar.And(q, Scalar<T>.One)), Scalar<T>.Two));
        return new OddFlat<T>(row, col);
    }

    private static CubeCoordinates<T> OffsetToCube(OddFlat<T> offset)
    {
        var q = offset.Col;
        var r = Scalar.Subtract(offset.Row, Scalar.Divide(Scalar.Subtract(offset.Col, Scalar.And(offset.Col, Scalar<T>.One)), Scalar<T>.Two));
        var s = Scalar.Subtract(Scalar.Negate(q), r);
        return new CubeCoordinates<T>(q, r, s);
    }
}