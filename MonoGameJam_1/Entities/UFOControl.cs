using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MonoGameJam_1
{
    class UFOControl : GameComponent
    {
        #region Fields
        GameLogic LogicRef;
        Camera CameraRef;
        List<UFO> TheUFOs = new List<UFO>();
        #endregion
        #region Properties
        List<UFO> UFOsRef { get => TheUFOs; }
        #endregion
        #region Constructor
        public UFOControl(Game game, Camera camera, GameLogic gameLogic) : base(game)
        {
            LogicRef = gameLogic;
            CameraRef = camera;

            game.Components.Add(this);
        }
        #endregion
        #region Initialize-Load-BeginRun
        public override void Initialize()
        {

            base.Initialize();
            LoadContent();
            BeginRun();
        }

        public void LoadContent()
        {

        }

        public void BeginRun()
        {
            TheUFOs.Add(new UFO(Game, CameraRef, LogicRef));
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
