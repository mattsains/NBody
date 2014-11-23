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

namespace NBody
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D orb;

        Universe universe = new Universe();

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
            /* MassObject sol = new MassObject(new Vector2Double(0, 0), new Vector2Double(0, 0), 2e30);
             MassObject earth = new MassObject(new Vector2Double(152e9, 0), new Vector2Double(0, 30e3), 6e24);
             MassObject venus = new MassObject(new Vector2Double(109e9, 0), new Vector2Double(0, 35e3), 4.8e24);
             MassObject mercury = new MassObject(new Vector2Double(70e9, 0), new Vector2Double(0, 47e3), 3e23);
             MassObject moon = new MassObject(new Vector2Double(152e9 + 405e6, 0), new Vector2Double(0, 30e3 + 1e3), 7.3477e22);
             MassObject mars = new MassObject(new Vector2Double(249e9, 0), new Vector2Double(0, 24e3), 6e23);
             MassObject jupiter = new MassObject(new Vector2Double(816e9, 0), new Vector2Double(0, 13e3), 1.896e27);
             MassObject ganymede = new MassObject(new Vector2Double(816e9 + 1e9, 0), new Vector2Double(0, 13e3 + 10.880e3), 1.4819e23);
             universe.Objects.Add(sol);
             universe.Objects.Add(earth);
             universe.Objects.Add(venus);
             universe.Objects.Add(mercury);
             universe.Objects.Add(moon);
             universe.Objects.Add(mars);
             universe.Objects.Add(jupiter);
             universe.Objects.Add(ganymede);*/

            MassObject a = new MassObject(new Vector2Double(-300e9, 0), new Vector2Double(0, 60e3), 1e31);
            MassObject b = new MassObject(new Vector2Double(-400e9, 0), new Vector2Double(0, -0e3), 1e31);
            MassObject c = new MassObject(new Vector2Double(400e9, 0), new Vector2Double(0, 0e3), 1e31);
            MassObject d = new MassObject(new Vector2Double(300e9, 0), new Vector2Double(0, -60e3), 1e31);

            universe.Objects.Add(a);
            universe.Objects.Add(b);
            universe.Objects.Add(c);
            universe.Objects.Add(d);
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

            orb = Content.Load<Texture2D>("orb");

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();
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
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < 1; i++)
                universe.Step(dt * 2628 * 500);

            scale = 1e-9;

            base.Update(gameTime);
        }



        double scale = 4e-9;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            Vector2 viewOffset = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            spriteBatch.Begin();
            foreach (MassObject o in universe.Objects)
            {
                int dotWidth = (int)(Math.Log10(Math.Pow(o.Mass, 1 / 3.0) * scale * 5000));
                Rectangle drawRectangle = new Rectangle((int)(scale * o.Position.X - dotWidth / 2.0 + viewOffset.X), (int)(scale * o.Position.Y - dotWidth / 2.0 + viewOffset.Y), dotWidth, dotWidth);
                spriteBatch.Draw(orb, drawRectangle, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
