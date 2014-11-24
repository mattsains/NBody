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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Universe universe = new Universe();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            /*
            Here are the details for a few masses in our own solar system.
            
            MassObject sol = new MassObject(new Vector2Double(0, 0), new Vector2Double(0, 0), 2e30);
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
            universe.Objects.Add(ganymede);
            
            */


            //here's details for a quad-star system rotating around barycentres
            MassObject a = new MassObject(new Vector2Double(-100e9, 0), new Vector2Double(0, 140e3), 1e31);
            MassObject b = new MassObject(new Vector2Double(-50e9, 0), new Vector2Double(0, 0), 1e31);
            MassObject c = new MassObject(new Vector2Double(50e9, 0), new Vector2Double(0, 0), 1e31);
            MassObject d = new MassObject(new Vector2Double(100e9, 0), new Vector2Double(0, -140e3), 1e31);
            universe.Objects.Add(a);
            universe.Objects.Add(b);
            universe.Objects.Add(c);
            universe.Objects.Add(d);

            base.Initialize();
        }

        Texture2D orb;

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

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit when escape is pressed
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            double dt = gameTime.ElapsedGameTime.TotalSeconds; //since the last update
            universe.Step(dt * 1314000); //one minute on screen is a year in the universe

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            double scale = 1e-9;
            Vector2 viewOffset = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            spriteBatch.Begin();
            foreach (MassObject o in universe.Objects)
            {
                int dotWidth = (int)(Math.Log10(Math.Pow(o.Mass, 1 / 3.0) * scale * 5000)); //tries to scale dots to the mass of objects without the perspective of space getting unmanageable.
                Rectangle drawRectangle = new Rectangle((int)(scale * o.Position.X - dotWidth / 2.0 + viewOffset.X), (int)(scale * o.Position.Y - dotWidth / 2.0 + viewOffset.Y), dotWidth, dotWidth);
                spriteBatch.Draw(orb, drawRectangle, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
