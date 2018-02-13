using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MonoGameJam_1
{
    class Roads : GameComponent
    {
        #region Fields
        GameLogic LogicRef;
        Camera CameraRef;
        Texture2D RoadTexture;
        List<PlaneEntity> TheRoads = new List<PlaneEntity>();
        #endregion
        #region Properties

        #endregion
        #region Constructor
        public Roads(Game game, Camera camera, GameLogic gameLogic) : base(game)
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
            RoadTexture = Helper.LoadTexture("Road");
        }

        public void BeginRun()
        {
            Setup();
        }
        #endregion
        #region Update
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        #endregion
        void Setup()
        {
            for (int i = 0; i < 14; i++)
            {
                TheRoads.Add(new PlaneEntity(Game, CameraRef, RoadTexture,
                    new Vector3(-(8 * 128) + (i * 128), 0.1f, 0), new Vector3(-MathHelper.PiOver2, 0, 0)));
            }
        }
    }
}
