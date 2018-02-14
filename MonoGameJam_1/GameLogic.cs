using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System;

public enum GameState
{
    Over,
    InPlay,
    HighScore,
    MainMenu
};

namespace MonoGameJam_1
{
    class GameLogic : GameComponent
    {
        Camera CameraRef;
        Terrain TheTerrain;
        Numbers ScoreDisplay;
        Letters WordDisplay;
        Effect TerrainEffect;
        Player ThePlayer;
        Houses TheHouses;
        Roads TheRoads;
        People ThePeople;
        UFOControl TheUFOs;

        GameState GameMode = GameState.InPlay;
        KeyboardState OldKeyState;

        public GameState CurrentMode { get => GameMode; }
        public Terrain TerrainRef { get => TheTerrain; }
        public People PeopleRef { get => ThePeople; }
        public UFOControl UFOControlRef { get => TheUFOs; }

        public GameLogic(Game game, Camera camera) : base(game)
        {
            CameraRef = camera;
            ScoreDisplay = new Numbers(game);
            WordDisplay = new Letters(game);
            ThePlayer = new Player(game, camera, this);
            TheHouses = new Houses(game, camera, this);
            TheRoads = new Roads(game, camera, this);
            ThePeople = new People(game, camera, this);
            TheUFOs = new UFOControl(game, camera, this);

            // Screen resolution is 1200 X 900.
            // Y positive is Up.
            // X positive is right of window when camera is at rotation zero.
            // Z positive is towards the camera when at rotation zero.
            // Positive rotation rotates CCW. Zero has front facing X positive. Pi/2 on Y faces Z negative.
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        public void LoadContent()
        {
            TerrainEffect = Game.Content.Load<Effect>("Effects/Terrain");

        }

        public void BeginRun()
        {
            TheTerrain = new Terrain(Game, CameraRef, TerrainEffect,
                "heightmap_flat_256", "Grass", "Rocky", "Snowy", 16, 5, 25);
            Cube box = new Cube(Game, CameraRef);

            ScoreDisplay.Setup(new Vector2(0, 200), 1);
            ScoreDisplay.SetNumber(100);
            WordDisplay.Setup(new Vector2(0, 250), 1);
            WordDisplay.SetWords("Test");
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState KBS = Keyboard.GetState();

            if (KBS != OldKeyState)
            {
            }


            OldKeyState = Keyboard.GetState();


            base.Update(gameTime);
        }
    }
}
