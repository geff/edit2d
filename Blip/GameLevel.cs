using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit2DEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Edit2DEngine.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FarseerGames.FarseerPhysics.Collisions;

namespace Blip
{
    public class GameLevel : GameBase
    {
        #region Attributs
        private Repository repository;
        private Render render;
        private string currentLevelFileName;

        private Entite blip;
        private float speed = 10f;
        #endregion

        public GameLevel(GameMain game, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager contentManager, string levelFilename)
            : base(game, spriteBatch, graphicsDevice, contentManager)
        {
            this.Game = game;

            Init();

            this.repository = new Repository();
            this.render = new Render(SpriteBatch, GraphicsDevice, repository, contentManager);
            this.currentLevelFileName = levelFilename;

            LoadLevel(levelFilename);

            InitLevel();
        }

        public override void Init()
        {
            //--- Mini menu
            ClickableText txtMenu = new ClickableText(this, "FontMenu_On", "FontMenu_Off", "Menu", new Vector2(10, 10));
            ClickableText txtRefresh = new ClickableText(this, "FontMenu_On", "FontMenu_Off", "Refresh", new Vector2(100, 10));

            txtMenu.ClickText += new ClickableText.ClickTextHandler(txtMenu_ClickText);
            txtRefresh.ClickText += new ClickableText.ClickTextHandler(txtRefresh_ClickText);

            this.AddClickableText(txtMenu);
            this.AddClickableText(txtRefresh);
            //---

            //---
            this.MenuAnimationOpenEnded += new MenuAnimationOpenEndedHandler(GameLevel_MenuAnimationOpenEnded);
            //---

            //---
            this.AddKeys(Microsoft.Xna.Framework.Input.Keys.Left);
            this.AddKeys(Microsoft.Xna.Framework.Input.Keys.Right);
            this.AddKeys(Microsoft.Xna.Framework.Input.Keys.Down);
            this.AddKeys(Microsoft.Xna.Framework.Input.Keys.Up);
            this.KeyPressed += new KeyPressedHandler(GameLevel_KeyPressed);

            this.MouseLeftButtonClicked += new MouseLeftButtonClickedHandler(GameLevel_MouseLeftButtonClicked);
            this.MouseMidddleButtonClicked += new MouseMidddleButtonClickedHandler(GameLevel_MouseMidddleButtonClicked);
            this.MouseRightButtonClicked += new MouseRightButtonClickedHandler(GameLevel_MouseRightButtonClicked);
            //---

            this.ShowMiniMenu = true;
            base.Init(false);
        }

        #region Évènements clavier et souris
        void GameLevel_KeyPressed(Microsoft.Xna.Framework.Input.Keys key, GameTime gameTime)
        {
            if (key == Keys.Up)
            {
                
            }
        }

        void GameLevel_MouseLeftButtonClicked(Microsoft.Xna.Framework.Input.MouseState mouseState, GameTime gameTime)
        {
        }
        void GameLevel_MouseMidddleButtonClicked(Microsoft.Xna.Framework.Input.MouseState mouseState, GameTime gameTime)
        {
        }

        void GameLevel_MouseRightButtonClicked(Microsoft.Xna.Framework.Input.MouseState mouseState, GameTime gameTime)
        {
        }
        #endregion

        #region Évènements menu
        void GameLevel_MenuAnimationOpenEnded(GameTime gameTime)
        {
            ((GameMain)this.Game).GameCurrent = new GameMenu((GameMain)this.Game, this.SpriteBatch, this.GraphicsDevice, this.ContentManager);
        }

        void txtRefresh_ClickText(ClickableText clickableText, Microsoft.Xna.Framework.Input.MouseState mouseState, GameTime gameTime)
        {
            LoadLevel(currentLevelFileName);

            InitLevel();
        }

        void txtMenu_ClickText(ClickableText clickableText, Microsoft.Xna.Framework.Input.MouseState mouseState, GameTime gameTime)
        {
            this.StartMenuOn(gameTime);
        }
        #endregion

        #region Load et Init level
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

        private void InitLevel()
        {
            blip = repository.listEntite.Find(e => e.Name == "Blip1");
        }

        #endregion

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //--- Keyboard
            KeyboardState keyBoardState = Keyboard.GetState();

            if (keyBoardState.IsKeyDown(Keys.Left))
            {
                blip.Body.ApplyImpulse(new Vector2(-speed, 0f));
            }
            if (keyBoardState.IsKeyDown(Keys.Right))
            {
                blip.Body.ApplyImpulse(new Vector2(speed, 0f));
            }

            if (keyBoardState.IsKeyDown(Keys.Down))
            {
                //blip.Body.ApplyImpulse(new Vector2(0f, -speed));
                blip.Body.ApplyForce(new Vector2(0f, speed));

            }
            if(keyBoardState.IsKeyDown(Keys.Up))
            {
                Jump();
            }
            //---

            repository.Camera.Position = blip.Position;
            render.Update();
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);

            Matrix mtxCamera = Matrix.Invert(repository.Camera.MatrixTransformation) * Matrix.CreateTranslation((float)this.GraphicsDevice.Viewport.Width / 2f - blip.SizeVector.X/2f, (float)this.GraphicsDevice.Viewport.Height / 2f - blip.SizeVector.Y/2f, 0f);

            this.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState, mtxCamera);

            render.Draw();

            this.SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void Jump()
        {
            bool canJump = false;

            for (float i = 1; i < 4 && !canJump; i++)
            {
                float angle = i * MathHelper.PiOver4;

                List<Vector2> listIntersections = new List<Vector2>();
                Vector2 startPoint = blip.Position + new Vector2(0, 0f);
                Vector2 endPoint = blip.Position + new Vector2(blip.SizeVector.Length() / 2f * (float)Math.Cos(angle), blip.SizeVector.Length() / 2f * (float)Math.Sin(angle));

                List<Geom> listGeomCollide = FarseerGames.FarseerPhysics.Collisions.CollisionHelper.LineSegmentAllGeomsIntersect(ref startPoint, ref endPoint, Repository.physicSimulator, false, ref listIntersections);

                if (listGeomCollide.FindAll(g=> g.CollisionEnabled).Count > 1)
                    canJump = true;
            }

            if (canJump)
                blip.Body.ApplyImpulse(new Vector2(0f, -30f * speed));
        }
    }
}
