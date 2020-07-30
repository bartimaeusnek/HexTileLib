using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
#if NETCOREAPP
using System.Text.Json.Serialization;
#endif
namespace HexTileLib.Coordinates
{
    public readonly struct CubeCoordinates : IEquatable<CubeCoordinates> , ICoordinates
    {
        public int q
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (int) this.Coords.X;
        }

        public int r
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (int) this.Coords.Y;
        }

        public int s
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (int) this.Coords.Z;
        }

#if NETCOREAPP
        [JsonIgnore]
        public Vector3 Coords
#elif NETSTANDARD2_0
        public (int X, int Y, int Z) Coords
#endif
        {
            get;
        }
        
        public CubeCoordinates(int q_, int r_, int s_)
        {
            if (q_ + r_ + s_ != 0)
            {
                throw new ArgumentException($"{nameof(CubeCoordinates)}: {nameof(q_)} + {nameof(r_)} + {nameof(s_)} != 0" );
            }
#if NETCOREAPP
            this.Coords = new Vector3(q_, r_, s_);
#elif NETSTANDARD2_0
            this.Coords = (q_, r_, s_);
#endif
        }

#if NETCOREAPP
        [JsonIgnore]
#endif
        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => LengthCalculation(this);
        }

#if NETCOREAPP
        [JsonIgnore]
#endif
        public CubeCoordinates RotateLeft
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => RotateLeftCalculation(this);
        }
        
#if NETCOREAPP
        [JsonIgnore]
#endif
        public CubeCoordinates RotateRight
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => RotateRightCalculation(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Distance(CubeCoordinates coordinates)
            => DistanceCalculation(this,coordinates);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubeCoordinates Neighbor(CoordinateDirectionFlat direction)
            => NeighborCalculation(this,(int) direction);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubeCoordinates Neighbor(CoordinateDirectionPointy direction)
            => NeighborCalculation(this, (int) direction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubeCoordinates DiagonalNeighbor(CoordinateDirectionFlatDiagonal direction)
            => DiagonalNeighborCalculation(this, (int) direction);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CubeCoordinates DiagonalNeighbor(CoordinateDirectionPointyDiagonal direction)
            => DiagonalNeighborCalculation(this, (int) direction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LengthCalculation(CubeCoordinates hex) => (Math.Abs(hex.q) + Math.Abs(hex.r) + Math.Abs(hex.s)) / 2;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DistanceCalculation(CubeCoordinates a, CubeCoordinates b) => LengthCalculation(a - b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeCoordinates RotateLeftCalculation(CubeCoordinates coordinates) => new CubeCoordinates(-coordinates.s, -coordinates.q, -coordinates.r);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeCoordinates RotateRightCalculation(CubeCoordinates coordinates) => new CubeCoordinates(-coordinates.r, -coordinates.s, -coordinates.q);

        public static List<CubeCoordinates> directions = new List<CubeCoordinates>{new CubeCoordinates(1, -1, 0), new CubeCoordinates(0, -1, 1), new CubeCoordinates(-1, 0, 1), new CubeCoordinates(-1, 1, 0), new CubeCoordinates(0, 1, -1),new CubeCoordinates(1, 0, -1)};
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeCoordinates Direction(int direction) => directions[direction];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeCoordinates NeighborCalculation(CubeCoordinates coordinates, int direction) => coordinates + Direction(direction);

        public static List<CubeCoordinates> diagonals = new List<CubeCoordinates>{new CubeCoordinates(2, -1, -1), new CubeCoordinates(1, -2, 1), new CubeCoordinates(-1, -1, 2), new CubeCoordinates(-2, 1, 1), new CubeCoordinates(-1, 2, -1), new CubeCoordinates(1, 1, -2)};
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeCoordinates Diagonals(int direction) => diagonals[direction];
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubeCoordinates DiagonalNeighborCalculation(CubeCoordinates coordinates, int direction) => coordinates + Diagonals(direction);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining 
#if NETCOREAPP
        | MethodImplOptions.AggressiveOptimization
#endif
                   )]
        public static List<CubeCoordinates> CubicDistanceCalculation(int range, CubeCoordinates center)
        {
            var results = new List<CubeCoordinates>();
            var (x1, y1, z1) = center;
            for (var x = -range + x1; x <= range + x1; x++)
            {
                for (var y = -range + y1; y <= range + y1; y++)
                {
                    for (var z = -range + z1; z <= range + z1; z++)
                    {
                        if (x + y + z != 0)
                            continue;
                        results.Add(new CubeCoordinates(x,y,z));
                    }
                }
            }

            return results;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<CubeCoordinates> Rings(int range) => RingsCalculation(range, this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<CubeCoordinates> CubicDistance(int range) => CubicDistanceCalculation(range, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining 
#if NETCOREAPP
        | MethodImplOptions.AggressiveOptimization
#endif
                   )]
        public static List<CubeCoordinates> RingsCalculation(int range, CubeCoordinates center)
        {
            var results = new List<CubeCoordinates>();
            var point = center.Neighbor((CoordinateDirectionFlat) 4);
            for (var i = 1; i < range; i++)
            {
                point = point.Neighbor((CoordinateDirectionFlat) 4);
            }

            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < range; j++)
                {
                    results.Add(point);
                    point = point.Neighbor((CoordinateDirectionFlat) i);
                }
            }

            return results;
        }
        
#region C#Specific
        public void Deconstruct(out int q, out int r, out int s)
        {
            q = this.q;
            r = this.r;
            s = this.s;
        }
        
#if NETCOREAPP
        private CubeCoordinates(Vector3 left) => this.Coords = left;

        public static implicit operator Vector3(CubeCoordinates left) => left.Coords;
        public static implicit operator CubeCoordinates(Vector3 left) => new CubeCoordinates(left);
        public static implicit operator Vector2(CubeCoordinates left) => (AxialCoordinates) left;
        public static implicit operator CubeCoordinates(Vector2 left) => (AxialCoordinates) left;
#endif
        
        public static implicit operator AxialCoordinates(CubeCoordinates left) => new AxialCoordinates(left.q, left.s);

        public static implicit operator CubeCoordinates(AxialCoordinates left) => new CubeCoordinates(left.q, left.s,left.r);
        
        public bool Equals(CubeCoordinates other) => this.Coords.Equals(other.Coords);

        public override bool Equals(object obj) => obj is CubeCoordinates other && this.Equals(other);

        public override int GetHashCode() => this.Coords.GetHashCode();

        public static bool operator ==(CubeCoordinates left, CubeCoordinates right) => left.Equals(right);
        public static bool operator !=(CubeCoordinates left, CubeCoordinates right) => !left.Equals(right);
        
#if NETCOREAPP
        public static CubeCoordinates operator +(CubeCoordinates left, CubeCoordinates right) => left.Coords + right.Coords;
        public static CubeCoordinates operator -(CubeCoordinates left, CubeCoordinates right) => left.Coords - right.Coords;
        public static CubeCoordinates operator *(CubeCoordinates left, int right) => left.Coords * right;
        public static CubeCoordinates operator /(CubeCoordinates left, int right) => left.Coords / right;
#elif NETSTANDARD2_0
        public static CubeCoordinates operator +(CubeCoordinates left, CubeCoordinates right) => new CubeCoordinates(left.Coords.X + right.Coords.X, left.Coords.Y + right.Coords.Y,left.Coords.Z + right.Coords.Z);
        public static CubeCoordinates operator -(CubeCoordinates left, CubeCoordinates right) => new CubeCoordinates(left.Coords.X - right.Coords.X, left.Coords.Y - right.Coords.Y,left.Coords.Z - right.Coords.Z);
        public static CubeCoordinates operator *(CubeCoordinates left, int right) => new CubeCoordinates(left.Coords.X * right, left.Coords.Y * right,left.Coords.Z * right);
        public static CubeCoordinates operator /(CubeCoordinates left, int right) => new CubeCoordinates(left.Coords.X / right, left.Coords.Y / right,left.Coords.Z / right);
#endif
        public override string ToString() => this.Coords.ToString();

#endregion
    }
}