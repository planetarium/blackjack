using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stellarium.Renderer.Tile
{
    public class HexagonTile
    {
        public struct Orientation
        {
            public float F0, F1, F2, F3;
            public float B0, B1, B2, B3;
            public float StartAngle; // multiples of 60 degree

            public Orientation(float f0, float f1, float f2, float f3,
                float b0, float b1, float b2, float b3,
                float startAngle)
            {
                F0 = f0;
                F1 = f1;
                F2 = f2;
                F3 = f3;
                B0 = b0;
                B1 = b1;
                B2 = b2;
                B3 = b3;
                StartAngle = startAngle;
            }
        }

        public int Q;
        public int R;
        public int S;

        private static readonly Orientation _orientation = new Orientation(
            Mathf.Sqrt(3.0f), Mathf.Sqrt(3.0f) / 2.0f, 0.0f, 3.0f / 2.0f,
            Mathf.Sqrt(3.0f) / 3.0f, -1.0f / 3.0f, 0.0f, 2.0f / 3.0f,
            0.5f); // 30 degree

        public HexagonTile(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public static bool operator ==(HexagonTile a, HexagonTile b)
        {
            return a.Q == b.Q && a.R == b.R && a.S == b.S;
        }

        public static bool operator !=(HexagonTile a, HexagonTile b)
        {
            return !(a == b);
        }

        public static HexagonTile operator +(HexagonTile a, HexagonTile b)
        {
            return new HexagonTile(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }

        public static HexagonTile operator -(HexagonTile a, HexagonTile b)
        {
            return new HexagonTile(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }

        public static HexagonTile operator *(HexagonTile a, HexagonTile b)
        {
            return new HexagonTile(a.Q * b.Q, a.R * b.R, a.S * b.S);
        }

        public static int Length(HexagonTile tile)
        {
            var q = Mathf.Abs(tile.Q);
            var r = Mathf.Abs(tile.R);
            var s = Mathf.Abs(tile.S);

            return (q + r + s) / 2;
        }

        public static int Distance(HexagonTile a, HexagonTile b)
        {
            return Length(a - b);
        }

        public static HexagonTile GetTile(Vector2 position)
        {
            var p = position *= 2;
            var q = Mathf.RoundToInt(_orientation.B0 * p.x + _orientation.B1 * -p.y);
            var r = Mathf.RoundToInt(_orientation.B2 * p.x + _orientation.B3 * -p.y);
            return new HexagonTile(q, r, -q - r);
        }

        public Vector2 GetPosition()
        {
            var x = _orientation.F0 * Q + _orientation.F1 * R;
            var y = _orientation.F2 * Q + _orientation.F3 * R;

            return new Vector2(x, -y);
        }

        public override bool Equals(object obj)
        {
            return (HexagonTile)obj == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            var position = GetPosition();
            return $"position : {position}, qsr : ({Q},{S},{R})";
        }
    }
}
