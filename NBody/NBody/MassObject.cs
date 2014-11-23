using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBody
{
    class MassObject
    {
        public Vector2Double Position { get; set; } //m
        public Vector2Double Velocity { get; set; } //m/s
        public double Mass { get; private set; } //kg

        public MassObject (Vector2Double position, Vector2Double velocity, double mass)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Mass = mass;
        }
    }
}
