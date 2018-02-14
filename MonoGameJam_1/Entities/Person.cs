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
    class Person : ModelEntity
    {
        #region Fields
        GameLogic LogicRef;
        ModelEntity[] Arms = new ModelEntity[2];
        ModelEntity[] Legs = new ModelEntity[2];
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Person(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            LogicRef = gameLogic;

            for (int i = 0; i < 2; i++)
            {
                Arms[i] = new ModelEntity(game, camera);
                Legs[i] = new ModelEntity(game, camera);
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
            LoadModel("Person-Body");

            for (int i = 0; i < 2; i++)
            {
                Arms[i].LoadModel("Person-Arm");
                Arms[i].AddAsChildOf(this);
                Arms[i].PO.Position.Y = 9;
                Legs[i].LoadModel("Person-Leg");
                Legs[i].AddAsChildOf(this);
            }

            base.LoadContent();
        }

        public override void BeginRun()
        {
            base.BeginRun();

            Arms[0].PO.Position.X = 6;
            Arms[1].PO.Position.X = -6;
            Legs[0].PO.Position.X = 2;
            Legs[1].PO.Position.X = -2;

            PO.Position.Y = 10;

            //Enabled = false;
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        #endregion
    }
}
