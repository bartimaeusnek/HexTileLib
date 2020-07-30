using System;
using System.Runtime.CompilerServices;
#if NETCOREAPP
using System.Text.Json.Serialization;
using System.Numerics;
#endif

namespace HexTileLib.Coordinates
{
    public readonly struct AxialCoordinates : IEquatable<AxialCoordinates> , ICoordinates
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

#if NETCOREAPP
        [JsonIgnore]
#endif
        public int s
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (int) (- this.Coords.X - this.Coords.Y);
        }

#if NETCOREAPP
        [JsonIgnore]

        public Vector2 Coords
#elif NETSTANDARD2_0
        public (int X, int Y) Coords
#endif
        {
            get;
        }
        
#if NETCOREAPP
        public AxialCoordinates(int q_, int r_) 
            => this.Coords = new Vector2(q_, r_);
        
        public static implicit operator Vector3(AxialCoordinates left) => (CubeCoordinates) left;
        public static implicit operator AxialCoordinates(Vector3 left) => (CubeCoordinates) left;
        public static implicit operator Vector2(AxialCoordinates left) => left.Coords;
        public static implicit operator AxialCoordinates(Vector2 left) => new AxialCoordinates((int) left.X,(int) left.Y);
#elif NETSTANDARD2_0
        public AxialCoordinates(int q_, int r_) 
            => this.Coords = (q_, r_);
#endif
        public bool Equals(AxialCoordinates other) => this.q == other.q && this.r == other.r;

        public override bool Equals(object obj) => obj is AxialCoordinates other && this.Equals(other);
#if NETCOREAPP
        public override int GetHashCode() => HashCode.Combine(this.q, this.r);
#elif NETSTANDARD2_0
        public override int GetHashCode() =>  17 + this.q.GetHashCode() * 23 + this.r.GetHashCode() * 31;
#endif
        public static bool operator ==(AxialCoordinates left, AxialCoordinates right) => left.Equals(right);

        public static bool operator !=(AxialCoordinates left, AxialCoordinates right) => !left.Equals(right);
        
        public void Deconstruct(out int q, out int r, out int s)
        {
            q = this.q;
            r = this.r;
            s = this.s;
        }
    }
}