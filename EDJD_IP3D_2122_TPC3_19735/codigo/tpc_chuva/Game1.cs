using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tpc_chuva
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        ClsPlano plano;
        ClsCirculo circulo;
        ClsParticula particula;
        ClsSystemParticula systemParticula;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            plano = new ClsPlano(_graphics.GraphicsDevice, Content.Load<Texture2D>("grass"));
            circulo = new ClsCirculo(_graphics.GraphicsDevice, 20, 5f, 5f, new Vector3(0f, 4f, 0f));
            particula = new ClsParticula(GraphicsDevice, new Vector3(0f, 0f, 0f), new Vector3(0.1f, 0.1f, 0.1f));
            systemParticula = new ClsSystemParticula(_graphics.GraphicsDevice, circulo);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            systemParticula.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            plano.Draw(_graphics.GraphicsDevice);
            circulo.Draw(_graphics.GraphicsDevice);
            systemParticula.Draw(_graphics.GraphicsDevice);
            base.Draw(gameTime);
        }
    }
}
