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
    class Player : Car
    {
        #region Fields
        GameLogic LogicRef;
        SteeringwheelUI TheSteeringWheel;
        ModelEntity Turret;
        ModelEntity[] FrontTires = new ModelEntity[2];
        ModelEntity[] RearTires = new ModelEntity[2];
        KeyboardState OldKeyState;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Player(Game game, Camera camera, GameLogic gameLogic) : base(game, camera, gameLogic)
        {
            LogicRef = gameLogic;
            Camera swCamera = new Camera(game, new Vector3(0, 0, 100), new Vector3(0, MathHelper.Pi, 0),
                GraphicsDevice.Viewport.AspectRatio, 90f, 110f);

            TheSteeringWheel = new SteeringwheelUI(game, swCamera, gameLogic);

            Turret = new ModelEntity(game, camera);

            for (int i = 0; i < 2; i++)
            {
                FrontTires[i] = new ModelEntity(game, camera);
                RearTires[i] = new ModelEntity(game, camera);
            }
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadModel("Car-Body");
            Turret.LoadModel("Car-Turret");
            Turret.Position = new Vector3(-2, 9, 0);
            Turret.AddAsChildOf(this);

            RearTires[0].PO.Position.Z = 7;
            RearTires[1].PO.Position.Z = -7;
            RearTires[1].PO.Rotation.Y = MathHelper.Pi;

            FrontTires[0].PO.Position.Z = 7;
            FrontTires[1].PO.Position.Z = -7;

            for (int i = 0; i < 2; i++)
            {
                FrontTires[i].LoadModel("Car-FrontTire");
                RearTires[i].LoadModel("Car-RearTire");

                FrontTires[i].PO.Position.X = 9;
                FrontTires[i].PO.Position.Y = 1;

                RearTires[i].PO.Position.X = -11.5f;
                RearTires[i].PO.Position.Y = 2;

                FrontTires[i].AddAsChildOf(this);
                RearTires[i].AddAsChildOf(this);
            }

            base.LoadContent();
        }

        public override void BeginRun()
        {
            base.BeginRun();
            FrontWheel = 9f;
            RearWheel = -11.5f;
            CameraRef.Position.Y = 100;
            PO.Position.Y = 5f;
            //Enabled = false;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {
            Input();
            StearingWheeAngle = StearingWheelTurn * MathHelper.PiOver4 * 0.045f;
            TheSteeringWheel.PO.Rotation.Z = StearingWheeAngle;

            FrontTires[0].PO.Rotation.Y = StearingWheeAngle * MathHelper.PiOver4 * 0.25f;
            FrontTires[1].PO.Rotation.Y = MathHelper.Pi +
                (StearingWheeAngle * MathHelper.PiOver4 * 0.25f);

            RearTires[0].PO.RotationVelocity.Z = -Speed * MathHelper.PiOver4;
            RearTires[1].PO.RotationVelocity.Z = Speed * MathHelper.PiOver4;

            FrontTires[0].PO.RotationVelocity.Z = -Speed * MathHelper.PiOver4;
            FrontTires[1].PO.RotationVelocity.Z = Speed * MathHelper.PiOver4;

            CameraRef.Position.Z = 200 + Position.Z;
            CameraRef.LookAt = Position;

            base.Update(gameTime);
        }
        #endregion
        void Input()
        {
            KeyboardState KBS = Keyboard.GetState();

            if (KBS != OldKeyState)
            {
                if (KBS.IsKeyDown(Keys.RightShift) && Speed < 5)
                {
                    switch (CurrentGear)
                    {
                        case Gear.First:
                            CurrentGear = Gear.Reverse;
                            EngineAccelerate = 0;
                            Transmission = 0;
                            Force.Velocity = Vector3.Zero;
                            Velocity = Vector3.Zero;
                            break;
                        case Gear.Reverse:
                            CurrentGear = Gear.First;
                            EngineAccelerate = 0;
                            Transmission = 0;
                            Force.Velocity = Vector3.Zero;
                            Velocity = Vector3.Zero;
                            break;
                    }
                }

                if (KBS.IsKeyDown(Keys.Enter))
                {
                    Reset();
                }
            }

            if (KBS.IsKeyDown(Keys.Up))
            {
                switch (CurrentGear)
                {
                    case Gear.First:
                    case Gear.Reverse:
                        EngineAccelerate++;
                        break;
                    case Gear.Second:
                        EngineAccelerate += 0.1f;
                        break;
                    case Gear.Third:
                        EngineAccelerate += 0.015f;
                        break;
                }

                if (EngineAccelerate > 100)
                {
                    EngineAccelerate = 100;
                }
            }
            else if (KBS.IsKeyDown(Keys.Down))
            {
                EngineAccelerate -= 1;

                switch (CurrentGear)
                {
                    case Gear.Second:
                        CurrentGear = Gear.First;
                        break;
                    case Gear.Third:
                        CurrentGear = Gear.Second;
                        break;
                }

                if (EngineAccelerate < 0)
                {
                    EngineAccelerate = 0;
                }
            }

            if (KBS.IsKeyDown(Keys.RightControl))
            {
                EngineAccelerate = 0;
                Transmission = 0;
            }

            if (KBS.IsKeyDown(Keys.Left))
            {
                StearingWheelTurn++;

                if (StearingWheelTurn > 100)
                {
                    StearingWheelTurn = 100;
                }
            }
            else if (KBS.IsKeyDown(Keys.Right))
            {
                StearingWheelTurn--;

                if (StearingWheelTurn < -100)
                {
                    StearingWheelTurn = -100;
                }
            }

            OldKeyState = Keyboard.GetState();
        }
    }
}
