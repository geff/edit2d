using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Edit2DEngine;
using System.IO;

namespace Blip
{
    public class GameMenu : GameBase
    {
        delegate void MenuItemDelegate(string level);
        MenuItemDelegate currentMenuItem = null;
        Texture2D texMenu;
        string selectedLevel;
        GameMain gameMain;

        public GameMenu(GameMain game, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(game, spriteBatch, graphicsDevice, contentManager)
        {
            this.Game = game;
            this.gameMain = game;

            Init();
        }

        #region Initialization
        public override void Init()
        {
            this.MenuAnimationCloseEnded += new MenuAnimationCloseEndedHandler(GameMenu_MenuAnimationCloseEnded);
            this.MenuAnimationOpenEnded += new MenuAnimationOpenEndedHandler(GameMenu_MenuAnimationOpenEnded);
            texMenu = ContentManager.Load<Texture2D>(@"Content\Pic\TextureSable");
            
            ClickableText txtTitle = new ClickableText(this, "FontTitle", "FontTitle", ".:| Blip |:.", new Vector2(10,GraphicsDevice.Viewport.Height / 4+10));
            this.AddClickableText(txtTitle);

            Vector2 pos = new Vector2(250, GraphicsDevice.Viewport.Height / 4 + 70);

            String[] files = Directory.GetFiles(@"..\..\..\..\Level\", "*.lvl");

            foreach (String file in files)
            {
                ClickableText txtLevel = new ClickableText(this, "FontMenu_On", "FontMenu_Off", Path.GetFileNameWithoutExtension(file), pos);
                txtLevel.ClickText += new ClickableText.ClickTextHandler(txtLevel_ClickText);
                this.AddClickableText(txtLevel);

                pos.Y += 25;

                if(pos.Y > ((float)GraphicsDevice.Viewport.Height *0.75f -25f))
                {
                    pos = new Vector2(pos.X+ 200,GraphicsDevice.Viewport.Height / 4 + 70);
                }
            }

            base.Init(true);
        }

        void GameMenu_MenuAnimationOpenEnded(GameTime gameTime)
        {
        }

        void txtLevel_ClickText(ClickableText clickableText, Microsoft.Xna.Framework.Input.MouseState mouseState, GameTime gameTime)
        {
            selectedLevel = clickableText.Text;
            currentMenuItem = new MenuItemDelegate(ShowLevel);//ShowLevel(clickableText.Text));
            this.StartMenuOff(gameTime);
        }

        void GameMenu_MenuAnimationCloseEnded(GameTime gameTime)
        {
            currentMenuItem(selectedLevel);
        }
        #endregion

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private void ShowLevel(string levelFileName)
        {
            gameMain.GameCurrent = new GameLevel(this.gameMain, this.SpriteBatch, GraphicsDevice, ContentManager, Path.Combine(@"..\..\..\..\Level", levelFileName + ".lvl"));
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(40, 30, 25));

            SpriteBatch.Begin();

            SpriteBatch.Draw(texMenu, new Vector2(0, 0), null, new Color(210,180,150));

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        #region Evènements
        #endregion
    }
}
