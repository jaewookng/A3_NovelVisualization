using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;  

namespace A3_NovelVisualization;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont _font;
    private List<string> _allUniqueWords;
    private List<string> _wordsToDisplay;
    private Random _random;
    private KeyboardState _prevKey; 
    private MouseState _prevMouse;
    
    private Color[] _colors = { Color.Gold, Color.White, Color.LightBlue };

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 700;
        _graphics.PreferredBackBufferHeight = 600;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _allUniqueWords = new List<string>();
        _random = new Random();

        using (var stream = TitleContainer.OpenStream("Content/uniquewords.txt"))
        using (var reader = new StreamReader(stream))
        {
            string fullText = reader.ReadToEnd();
            string[] splitWords = fullText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            _allUniqueWords.AddRange(splitWords);
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("MyFont"); 
    }

    private void RefreshWordCloud()
    {
        _wordsToDisplay.Clear();
        for (int i = 0; i < 40 && i < _allUniqueWords.Count; i++)
        {
            int index = _random.Next(_allUniqueWords.Count);
            _wordsToDisplay.Add(_allUniqueWords[index]);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState currentKey = Keyboard.GetState();
        MouseState currentMouse = Mouse.GetState();

        if (currentKey.IsKeyDown(Keys.Escape)) Exit();
        
        if ((currentMouse.LeftButton == ButtonState.Pressed && _prevMouse.LeftButton == ButtonState.Released) ||
            (currentKey.IsKeyDown(Keys.Enter) && _prevKey.IsKeyUp(Keys.Enter)))
        {
            RefreshWordCloud();
        }

        _prevKey = currentKey; 
        _prevMouse = currentMouse;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        int x = 50;
        int y = 50;

        foreach (string word in _wordsToDisplay)
        {
            Vector2 size = _font.MeasureString(word + "  ");
            
            if (x + size.X > 650) 
            {
                x = 50;
                y += (int)size.Y + 10;
            }

            if (y + size.Y < 580) 
            {
                Color displayColor = _colors[_random.Next(_colors.Length)];
                _spriteBatch.DrawString(_font, word, new Vector2(x, y), displayColor);
                x += (int)size.X;
            }
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}