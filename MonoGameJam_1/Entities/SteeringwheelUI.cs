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
    class SteeringwheelUI : ModelEntity
    {
        #region Fields
        GameLogic LogicRef;

        #endregion
        #region Properties

        #endregion
        #region Constructor
        public SteeringwheelUI(Game game, Camera camera, GameLogic gameLogic) : base(game, camera)
        {
            LogicRef = gameLogic;

        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadModel("Steeringwheel");

            base.LoadContent();
        }

        public override void BeginRun()
        {
            base.BeginRun();
            PO.Position.Y = 30;
            ModelScale = new Vector3(0.5f);

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
