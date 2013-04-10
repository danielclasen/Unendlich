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


namespace Unendlich
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //dac comment pull request test
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int startScreenBreite = 1600;
        int startScreenHoehe = 900;
        
        //temporär
        SpriteFont pericles14;
        //Asteroidenfeld asteroidenfeld;

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
            graphics.PreferredBackBufferWidth = startScreenBreite;
            graphics.PreferredBackBufferHeight = startScreenHoehe;
            graphics.ApplyChanges();

            Kamera.sichtfeldBreite = startScreenBreite;
            Kamera.sichtfeldHoehe = startScreenHoehe;
            Kamera.Init(Vector2.Zero);//noch nicht optimal, je nach anforderung

            Spielmanager.LoadContent(Content);//muss vorher initialisiert werden, da ansonsten dem Spieler keine Textur zugeordnet werden kann

            Spielmanager.Init();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pericles14 = Containerklasse.GebeSchrift("pericles14");

            //temporär
            //asteroidenfeld = new Asteroidenfeld(new Rectangle(0, 0, 10000, 10000));
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
            Spielmanager.UpdateIngame(gameTime);

            //temporär
            //asteroidenfeld.Update(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            
            spriteBatch.DrawString(pericles14, (gameTime.ElapsedGameTime.TotalSeconds).ToString(), Vector2.Zero, Color.White);

            Spielmanager.DrawIngame(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
