using FusionEngine.Engine.Rendering;
using System;
using System.Collections.Generic;

namespace FusionEngine.Engine
{
    public class Vector
    {
        public float x, y;

        public Vector(float x = 0, float y = 0)
        {
            this.x = x;
            this.y = y;
        }

        private float magnitude
        {
            get
            {
                return (float)Math.Sqrt((x * x) + (y * y));
            }
        }

        public Vector normalize
        {
            get
            {
                Vector norm = new Vector(x /= magnitude, y /= magnitude);
                if (!float.IsNaN(norm.x) || !float.IsNaN(norm.y))
                    return norm;
                else
                    return new Vector(0, 0);
            }
        }
    }
}
