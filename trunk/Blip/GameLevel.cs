using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Edit2DEngine.Render;
using Microsoft.Xna.Framework;

namespace Blip
{
    public class GameLevel : GameBase
    {
        private Repository repository;
        private Render render;

        public GameLevel(GameMain game, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager contentManager, string levelFilename)
            : base(game, spriteBatch, graphicsDevice, contentManager)
        {
            this.Game = game;

            Init();

            this.repository = new Repository();
            this.render = new Render(SpriteBatch, GraphicsDevice, repository);

            LoadLevel(levelFilename);
        }

        private void LoadLevel(string levelFilename)
        {
            FileSystem.Open(levelFilename, repository);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            render.Update();

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            this.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            render.Draw();

            base.Draw(gameTime);

            this.SpriteBatch.End();
        }
    }
}
