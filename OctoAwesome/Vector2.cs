﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoAwesome
{
    
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 NaN { get { return new Vector2(float.NaN, float.NaN); } }

        public float LengthSquare()
        {
            return (X * X) + (Y * Y);
        }

        public float Length()
        {
            return (float)Math.Sqrt(LengthSquare());
        }

        public float Angle()
        {
            return (float)Math.Atan2(Y, X);
        }

        public Vector2 Normalized()
        {
            float length = Length();
            if (length == 0)
            {
                return Vector2.NaN;
            }
            else
            {
                // Vector2 squareThis = new Vector2(this.X * this.X, this.Y * this.Y);
                return (this / length);
            }
            
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v1, float scale)
        {
            return new Vector2(v1.X * scale, v1.Y * scale);
        }

        public static Vector2 operator /(Vector2 v1, float scale)
        {
            return new Vector2(v1.X / scale, v1.Y / scale);
        }

    }
}
