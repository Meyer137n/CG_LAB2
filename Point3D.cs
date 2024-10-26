using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LAB2
{
    // Класс для 3D точки
    public class Point3D
    {
        private float x, y, z;
        public Point3D(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }
        public float getX()
        {
            return x;
        }
        public float getY()
        {
            return y;
        }
        public float getZ()
        {
            return z;
        }
    }
}
