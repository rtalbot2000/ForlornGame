using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Forlorn
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Bats[] bats = new Bats[10];
        Level level;

        Texture2D testPixel;

        Camera camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 16 * 100;
            graphics.PreferredBackBufferHeight = 16 * 60;
            graphics.ApplyChanges();

            camera = new Camera(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Loads player
            player = new Player(50, graphics.PreferredBackBufferHeight / 2, this.Content);
            for(int i = 0; i < bats.Length; i++)
                bats[i] = new Bats(this.Content, player.Position);
            // TODO: use this.Content to load your game content here
            testPixel = Content.Load<Texture2D>("test/BlockTestPixel");

            this.level = new Level(testPixel, player);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState kb = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                kb.IsKeyDown(Keys.Escape))
                this.Exit();
            //Updates controlled movements of player
            player.update(kb);
            for (int i = 0; i < bats.Length; i++)
                bats[i].batUpdate(kb);
            // TODO: Add your update logic here

            level.Update(player);

            camera.Update(player.Position);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(147, 201, 227));

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.ViewMatrix);

            if(!player.isDead())
                spriteBatch.Draw(player.getTexture(), player.getRect(), Color.WhiteSmoke);
            
            level.Draw(player.Position, spriteBatch, gameTime);

            for (int i = 0; i < bats.Length; i++)
                spriteBatch.Draw(bats[i].getTexture(), bats[i].getRect(), Color.WhiteSmoke);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
