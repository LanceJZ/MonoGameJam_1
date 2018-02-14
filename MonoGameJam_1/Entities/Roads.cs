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
            //int gridSize = 6400;
            int roadSize = 128;
            int roadCount = 40;
            int roadColormSpace = 640;
            int roadColums = 7;
            int roadRowSpace = 1600;
            int roadRows = 4;
            int roadPerRow = 4;
            int roadBlocks = roadColums + 1;

            for (int c = 0; c < roadColums; c++)
            {
                for (int i = 0; i < roadCount; i++)
                {
                    TheRoads.Add(new PlaneEntity(Game, CameraRef, RoadTexture,
                        new Vector3(-((roadCount / 2) * roadSize) + (i * roadSize), 0.25f,
                        -((roadColums / 2) * roadColormSpace) + (c * roadColormSpace)),
                        new Vector3(-MathHelper.PiOver2, 0, 0)));
                }
            }

            for (int r = 0; r < roadRows; r++)
            {
                int roadPlacement = 0;

                for (int b = 0; b < roadBlocks; b++)
                {
                    for (int i = 0; i < roadPerRow; i++)
                    {
                        TheRoads.Add(new PlaneEntity(Game, CameraRef, RoadTexture,
                            new Vector3(-((roadRows / 2) * roadRowSpace) + (r * roadRowSpace),
                            0.25f,(-(roadPerRow * 1.75f) * roadSize) +
                            -((roadBlocks / 2) * ((roadSize * roadPerRow) / 2)) +
                            -((roadPerRow / 2) * roadSize) +
                            -((roadRows / 2) * roadSize) +
                            (i * roadSize) + (roadPlacement * roadSize) + (b * (roadSize * roadPerRow))),
                            new Vector3(-MathHelper.PiOver2, MathHelper.PiOver2, 0)));
                    }

                    roadPlacement++;
                }
            }
        }
    }
}
