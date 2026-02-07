using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A3_NovelVisualization;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    // add conditional for swapping between display functions
    private bool _wordMode = true;
    private void TextInputCallback(object sender, TextInputEventArgs args)
    {
        if (args.Character == '\r')
        {
            if (_wordMode)
            {
                _wordMode = false;
            }
            else
            {
                _wordMode = true;
            }
        }
    }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Window.TextInput += TextInputCallback;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        // draw call either of the two display functions - ideally two public classes with their own display method
        _spriteBatch.Begin();
        if (_wordMode)
        {
            displayAllWords();
        }
        else
        {
            displayAllWords();
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
