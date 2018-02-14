using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MonoGameJam_1
{
    class Houses : GameComponent
    {
        #region Fields
        GameLogic LogicRef;
        Camera CameraRef;
        Model HouseModel;
        List<ModelEntity> TheHouses = new List<ModelEntity>();
        #endregion
        #region Properties
        public List<ModelEntity> HousesRef { get => TheHouses; }
        #endregion
        #region Constructor
        public Houses(Game game, Camera camera, GameLogic gameLogic) : base(game)
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
            HouseModel = Helper.LoadModel("HouseOne");
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
            int spaceBetween = 200;
            int spaceBetweenColums = 640;
            int spaceBetweenBlocks = 1600;
            int perBlock = 7;
            int blocks = 2;
            int colums = 7;

            for (int c = 0; c < colums; c++)
            {
                for (int b = 0; b < blocks; b++)
                {
                    for (int i = 0; i < perBlock; i++)
                    {
                        TheHouses.Add(new ModelEntity(Game, CameraRef, HouseModel,
                            new Vector3(-(spaceBetweenBlocks / 2) + -(spaceBetween / 2) +
                            -((spaceBetween * perBlock) / 2) +
                            (i * spaceBetween) + (b * spaceBetweenBlocks) + spaceBetween, 0.0f,
                            -(spaceBetween * 2.5f) + (colums * (spaceBetweenColums / 2)) +
                            -((spaceBetweenColums * c)))));
                        TheHouses.Last().ModelScale = new Vector3(2);
                    }
                }
            }
        }
    }
}
