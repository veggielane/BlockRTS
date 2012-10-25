﻿using System;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Physics
{
    public class Body:IBody
    {
        public double Mass { get { return 1.0; } }
        public Vect3 Position { get; set; }
        public Quat Rotation { get; set; }

        public Vect3 Velocity { get; private set; }

        public Body(Vect3 position, Quat rotation)
        {
            Position = position;
            Rotation = rotation;

            var rnd = new Random();
            Velocity = new Vect3(rnd.NextDouble(-5.0, 5.0), rnd.NextDouble(-5.0, 5.0), rnd.NextDouble(-5.0, 5.0));
        }

        public void ApplyImpulse()
        {

        }


        private Random _random = new Random();
        public void Update(TickTime delta)
        {
            Position = Position + (Velocity * delta.GameTimeDelta.TotalSeconds);


            Rotation *= new Euler(Angle.FromDegrees(_random.Next(10) * delta.GameTimeDelta.TotalSeconds), Angle.FromDegrees(_random.Next(10) * delta.GameTimeDelta.TotalSeconds), Angle.FromDegrees(_random.Next(10) * delta.GameTimeDelta.TotalSeconds)).ToQuat();
        }
    }
}
