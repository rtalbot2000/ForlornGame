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
        MouseState oldMouse;

        private Sword sword;
        private Axe axe;
        private Pickaxe pickaxe;
        SpriteFont damageText;
        public int batHealth;
        Player player;
        Random randomGen = new Random();
        List<Bats> allBats;
        Item item;
        Camera camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();
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
            IsMouseVisible = true;
            oldMouse = Mouse.GetState();

            allBats = new List<Bats>();

            IsMouseVisible = true;

            camera = new Camera(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            player = new Player(50, graphics.PreferredBackBufferHeight / 2, this.Content);
            for (int i = 0; i < 100; i++)
            {
                allBats.Add(new Bats(this.Content, player.getPosition()));
                allBats[i].setPosition(new Vector2(randomGen.Next(4800), 50));
                int randomVel;
                if (allBats[i].spawned)
                {
                    randomVel = (int)Math.Round((double)randomGen.Next(1));
                    if (randomVel == 0) 
                        randomVel = -1 * randomGen.Next(10) + 5;
                    else
                        randomVel = randomGen.Next(10) + 5;
                    allBats[i].setVelocity(randomVel);
                }
            }
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Loads player
            player = new Player(50, graphics.PreferredBackBufferHeight / 2, this.Content);
            // TODO: use this.Content to load your game content here

            damageText = this.Content.Load<SpriteFont>("DamageText");
            sword = new Sword(1, true, null, 300, graphics.PreferredBackBufferHeight / 2, -45, this.Content, 4);
            axe = new Axe(100, graphics.PreferredBackBufferHeight / 2, -45, this.Content, 2);
            pickaxe = new Pickaxe(150, graphics.PreferredBackBufferHeight / 2, 0, this.Content, 2);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //Updates controlled movements of player
            KeyboardState kb = Keyboard.GetState();

            player.update(kb);
            for (int i = 0; i < allBats.Count; i++)
            {
                allBats[i].batUpdate(kb, player.getPosition());
            }
            // TODO: Add your update logic here
            MouseState mouse = Mouse.GetState();
            axe.Update(allBats);
            pickaxe.Update(allBats);
            sword.Update(allBats);
            oldMouse = mouse;
            
            level.Update(Mouse.GetState());

            camera.Update(player);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < allBats.Count; i++)
                spriteBatch.Draw(allBats[i].getTexture(), allBats[i].getRect(), Color.WhiteSmoke);
                spriteBatch.Draw(player.getTexture(), player.getRect(), Color.WhiteSmoke);
            sword.Draw(gameTime, spriteBatch);
            axe.Draw(gameTime, spriteBatch);
            pickaxe.Draw(gameTime, spriteBatch);
            for (int i = 0; i < allBats.Count; i++)
            {

                spriteBatch.Draw(allBats[i].getTexture(), allBats[i].getRect(), Color.White);
                //spriteBatch.DrawString(damageText,  + "", new Vector2(0, 100), Color.Red);
            }
            player.Inventory.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
