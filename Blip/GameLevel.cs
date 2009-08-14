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
        private string currentLevelFileName;

        public GameLevel(GameMain game, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager contentManager, string levelFilename)
            : base(game, spriteBatch, graphicsDevice, contentManager)
        {
            this.Game = game;

            Init();

            this.repository = new Repository();
            this.render = new Render(SpriteBatch, GraphicsDevice, repository);
            this.currentLevelFileName = levelFilename;

            LoadLevel(levelFilename);
        }

        public override void Init()
        {
            //--- Mini menu
            ClickableText txtMenu = new ClickableText(this, "FontMenu_On", "FontMenu_Off", "Menu", new Vector2(10, 10));
            ClickableText txtRefresh = new ClickableText(this, "FontMenu_On", "FontMenu_Off", "Refresh", new Vector2(100, 10));

            txtMenu.ClickText+=new ClickableText.ClickTextHandler(txtMenu_ClickText);
            txtRefresh.ClickText+=new ClickableText.ClickTextHandler(txtRefresh_ClickText);
            
            this.AddClickableText(txtMenu);
            this.AddClickableText(txtRefresh);
            //---

            //---
            this.MenuAnimationOpenEnded += new MenuAnimationOpenEndedHandler(GameLevel_MenuAnimationOpenEnded);
            //---

            this.ShowMiniMenu = true;
            base.Init(false);
        }

        void GameLevel_MenuAnimationOpenEnded(GameTime gameTime)
        {
            ((GameMain)this.Game).GameCurrent = new GameMenu((GameMain)this.Game, this.SpriteBatch, this.GraphicsDevice, this.ContentManager);
        }

        void txtRefresh_ClickText(ClickableText clickableText, Microsoft.Xna.Framework.Input.MouseState mouseState, GameTime gameTime)
        {
            LoadLevel(currentLevelFileName);
        }

        void txtMenu_ClickText(ClickableText clickableText, Microsoft.Xna.Framework.Input.MouseState mouseState, GameTime gameTime)
        {
            this.StartMenuOn(gameTime);
        }

        private void LoadLevel(string levelFilename)
        {
            try
            {
                FileSystem.Open(levelFilename, repository);
            }
            catch (Exception ex)
            {
                this.StartMenuOn(((GameMain)this.Game).LastGameTime);
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            render.Update();

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);

            this.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            render.Draw();

            base.Draw(gameTime);

            this.SpriteBatch.End();
        }
    }
}
