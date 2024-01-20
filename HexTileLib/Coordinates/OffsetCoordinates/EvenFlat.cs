namespace HexTileLib.Coordinates.OffsetCoordinates;

using System;
using Silk.NET.Maths;

public readonly struct EvenFlat<T>(T row, T col) : ICoordinates<T>, IEquatable<EvenFlat<T>>
    where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
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
    public bool Equals(EvenFlat<T> other)
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
        return Equals((EvenFlat<T>)obj);
    }
    public override int GetHashCode()
    {
        return _vector2.GetHashCode();
    }
    public static bool operator ==(EvenFlat<T> left, EvenFlat<T> right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(EvenFlat<T> left, EvenFlat<T> right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return _vector2.ToString();
    }
#endregion

    public static explicit operator CubeCoordinates<T>(EvenFlat<T> evenFlat)
    {
        return evenFlat.OffsetToCube();
    }
    public static explicit operator EvenFlat<T>(CubeCoordinates<T> evenColumn)
    {
        return CubeToOffset(evenColumn);
    }
    public static explicit operator EvenFlat<T>(EvenPointy<T> evenPointy)
    {
        return (EvenFlat<T>)(CubeCoordinates<T>)evenPointy;
    }
    public static explicit operator EvenFlat<T>(OddFlat<T> evenRow)
    {
        return (EvenFlat<T>)(CubeCoordinates<T>)evenRow;
    }
    public static explicit operator EvenFlat<T>(OddPointy<T> evenPointy)
    {
        return (EvenFlat<T>)(CubeCoordinates<T>)evenPointy;
    }
    public static explicit operator AxialCoordinates<T>(EvenFlat<T> evenFlat)
    {
        return (AxialCoordinates<T>)evenFlat.OffsetToCube();
    }
    public static explicit operator EvenFlat<T>(AxialCoordinates<T> evenColumn)
    {
        return CubeToOffset((CubeCoordinates<T>)evenColumn);
    }

    private static EvenFlat<T> CubeToOffset(CubeCoordinates<T> cubeCoordinates)
    {
        var (q, r, _) = cubeCoordinates;
        var col = q;
        var row = Scalar.Add(r, Scalar.Divide(Scalar.Add(q, Scalar.And(q, Scalar<T>.One)), Scalar<T>.Two));
        return new EvenFlat<T>(row, col);
    }

    private CubeCoordinates<T> OffsetToCube()
    {
        var q = Col;
        var r = Scalar.Subtract(Row, Scalar.Divide(Scalar.Add(Col, Scalar.And(Col, Scalar<T>.One)), Scalar<T>.Two));
        var s = Scalar.Subtract(Scalar.Negate(q), r);
        return new CubeCoordinates<T>(q, r, s);
    }
}