using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBody
{
    class Universe
    {
        const double G = 6.67384e-11;
        public List<MassObject> Objects = new List<MassObject>();

        public void Step (double dt)
        {
            if (dt == 0) return;

            for (int i = 0; i < Objects.Count - 1; i++)
            {
                MassObject a = Objects[i];
                for (int j = i + 1; j < Objects.Count; j++)
                {
                    MassObject b=Objects[j];
                    Vector2Double relposition=a.Position-b.Position;
                    Vector2Double force = (G * a.Mass * b.Mass / (Math.Pow(relposition.Length(), 3))) * relposition;

                    a.Velocity -= dt * force / a.Mass;
                    b.Velocity += dt * force / b.Mass;
                }
            }
            foreach (MassObject o in Objects)
            {
                o.Position+=o.Velocity*dt;
            }
        }
    }
}
