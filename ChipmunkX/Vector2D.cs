﻿using System;

using ChipmunkX.Native;

namespace ChipmunkX
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public static readonly Vector2D Zero = new Vector2D();

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double LengthSquared => X * X + Y * Y;

        public double Length => Math.Sqrt(LengthSquared);

        public double Angle => Math.Atan2(Y, X);

        public double Cross(Vector2D vector) => Cross(this, vector);
        public double Distance(Vector2D vector) => Distance(this, vector);

        public static double Cross(Vector2D left, Vector2D right) => left.X * right.Y - left.Y * right.X;
        public static double Distance(Vector2D left, Vector2D right) => (left - right).Length;

        public static implicit operator cpVect(Vector2D vector) => new cpVect(vector.X, vector.Y);
        public static implicit operator Vector2D(cpVect vector) => new Vector2D(vector.x, vector.y);

        public static Vector2D operator +(Vector2D left, Vector2D right) => new Vector2D(left.X + right.X, left.Y + right.Y);
        public static Vector2D operator -(Vector2D left, Vector2D right) => new Vector2D(left.X - right.X, left.Y - right.Y);
        public static Vector2D operator -(Vector2D vector) => new Vector2D(-vector.X, -vector.Y);
        public static Vector2D operator *(Vector2D vector, double scalar) => new Vector2D(vector.X * scalar, vector.Y * scalar);
        public static Vector2D operator *(double scalar, Vector2D vector) => new Vector2D(vector.X * scalar, vector.Y * scalar);
        public static double operator *(Vector2D left, Vector2D right) => left.X * right.X + left.Y * right.Y;
        public static Vector2D operator /(Vector2D vector, double scalar) => new Vector2D(vector.X / scalar, vector.Y / scalar);

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vector2D))
                return false;
            return Equals((Vector2D)obj);
        }

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
        public bool Equals(Vector2D other) => this.X == other.X && this.Y == other.Y;
        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);
        public static bool operator !=(Vector2D left, Vector2D right) => !left.Equals(right);
    }
}
