#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;
#endregion
namespace MonoGameJam_1
{
    enum Gear
    {
        First,
        Second,
        Third,
        Reverse
    };

    class Car : ModelEntity
    {
        #region Fields
        protected GameLogic LogicRef;
        protected PositionedObject Force;
        protected Gear CurrentGear;

        protected float Speed;
        protected float MaxSpeed;
        protected float Tracktion;
        protected float CarHeading;
        protected float DistanceOneFrame;
        protected float EngineAccelerate;
        protected float Transmission;
        protected float GearRatio;
        protected float StearingWheelTurn;
        protected float StearingWheeAngle;
        protected float FrontWheel;
        protected float RearWheel;
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Car(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            LogicRef = gameLogic;
            Force = new PositionedObject(game); //The car's inertia.
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //LoadModel("");

            base.LoadContent();
        }

        public override void BeginRun()
        {
            base.BeginRun();
            Reset();
            //Enabled = false;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            Move();

            base.Update(gameTime);
        }
        #endregion
        public virtual void Reset()
        {
            Velocity = Vector3.Zero;
            Acceleration = Vector3.Zero;
            Force.Velocity = Vector3.Zero;
            CurrentGear = Gear.First;
            GearRatio = 0.75f;
            MaxSpeed = 120;
            EngineAccelerate = 0;
            Transmission = 0;
            StearingWheelTurn = 0;
            Tracktion = 15.5f;
        }

        public virtual void Bumped(Vector3 position, Vector3 velocity)
        {
            Acceleration = Vector3.Zero;
            Velocity = (Velocity * 0.1f) * -1;
            Velocity += velocity * 0.5f;
            float hitForce = Vector3.Distance(Vector3.Zero, velocity) * 0.5f;
            Velocity += PO.VelocityFromVectorsY(position, Position, hitForce);
        }

        void Move()
        {
            DistanceOneFrame = Vector3.Distance(Position, Position + Velocity);
            float groundlevel = LogicRef.TerrainRef.GetHeight(Position);

            if (Position.Y < groundlevel)
            {
                Position += -Velocity / 5;
                Velocity = Vector3.Zero;
                Acceleration = Vector3.Zero;
                Force.Velocity = Vector3.Zero;
                EngineAccelerate = 0;
                Transmission = 0;
            }
            else
            {
                if (EngineAccelerate < Transmission)
                {
                    Transmission -= 20 * PO.ElapsedGameTime;
                }

                if (MaxSpeed > Speed)
                    Transmission += (EngineAccelerate / (GearRatio * 2)) * PO.ElapsedGameTime;

                if (Transmission > 100)
                    Transmission = 100;

                Speed = DistanceOneFrame * 0.15f;

                float steerAngle;

                if (CurrentGear == Gear.Reverse)
                {
                    steerAngle = StearingWheeAngle * -0.025f;
                }
                else
                {
                    if (Speed > 10)
                    {
                        steerAngle = StearingWheeAngle *
                        (0.015f / ((Speed + 1) * 0.05f));
                    }
                    else if (Speed > 0)
                    {
                        steerAngle = StearingWheeAngle * 0.025f;
                    }
                    else
                    {
                        steerAngle = 0;
                    }
                }

                Vector2 carLocation = new Vector2(Position.X, Position.Z);
                Vector2 frontWheel = carLocation + FrontWheel * new
                    Vector2((float)Math.Cos(CarHeading), (float)Math.Sin(CarHeading));
                Vector2 backWheel = carLocation + RearWheel * new
                    Vector2((float)Math.Cos(CarHeading), (float)Math.Sin(CarHeading));
                backWheel += Speed * new
                    Vector2((float)Math.Cos(CarHeading), (float)Math.Sin(CarHeading));
                frontWheel += Speed * new Vector2((float)Math.Cos(CarHeading + steerAngle),
                    (float)Math.Sin(CarHeading + steerAngle));

                float forceAngel = CarHeading + steerAngle + MathHelper.Pi;
                Vector3 steeringForce = PO.VelocityFromAngleY(forceAngel, Speed);

                CarHeading = (float)Math.Atan2(frontWheel.Y - backWheel.Y, frontWheel.X - backWheel.X);

                PO.Rotation.Y = CarHeading;

                float forceAmount = (Tracktion * (Transmission * GearRatio)) + (Speed * 20);

                if (CurrentGear == Gear.Reverse)
                {
                    Force.Velocity = PO.VelocityFromAngleY(CarHeading, -forceAmount * 0.25f);
                }
                else
                {
                    Force.Velocity = PO.VelocityFromAngleY(CarHeading, forceAmount);
                }

                if (Vector3.Distance(Velocity, Force.Velocity) > 10.5f)
                {
                    Acceleration = Force.Velocity;
                }
                else
                {
                    Velocity = Force.Velocity;
                    Acceleration = Vector3.Zero;
                }

                float speedAdj = 0;

                if (StearingWheeAngle > 0)
                {
                    speedAdj = StearingWheeAngle * 20;
                }
                else if (StearingWheeAngle < 0)
                {
                    speedAdj = -StearingWheeAngle * 20;
                }

                if (Speed > (MaxSpeed - speedAdj))
                {
                    Acceleration = -Velocity;
                    Transmission = Speed * 0.75f;
                }
                else
                {
                    Acceleration -= (Velocity * 4);
                }

                PO.Acceleration.Y = 0;
                PO.Velocity.Y = 0;
            }
        }

        void SwitchGears()
        {
            switch (CurrentGear)
            {
                case Gear.First:
                    if (Speed > 25)
                    {
                        System.Diagnostics.Debug.WriteLine("Second Gear. Speed " + Speed.ToString());
                        CurrentGear = Gear.Second;
                        GearRatio = 2.75f;
                        Transmission -= 10;
                    }
                    else
                    {
                        GearRatio = 1.25f;
                    }
                    break;
                case Gear.Second:
                    if (Speed > 40f)
                    {
                        System.Diagnostics.Debug.WriteLine("Third Gear. Speed " + Speed.ToString());
                        CurrentGear = Gear.Third;
                        Transmission -= 10;
                        GearRatio = 3.75f;
                    }
                    else if (Speed < 20)
                    {
                        CurrentGear = Gear.First;
                        System.Diagnostics.Debug.WriteLine("First Gear. Speed " + Speed.ToString());
                    }
                    break;
                case Gear.Third:
                    if (Speed < 30f)
                    {
                        CurrentGear = Gear.Second;
                        System.Diagnostics.Debug.WriteLine("Second Gear. Speed " + Speed.ToString());
                    }
                    break;
                case Gear.Reverse:
                    GearRatio = -0.75f;
                    break;
            }
        }
    }
}
