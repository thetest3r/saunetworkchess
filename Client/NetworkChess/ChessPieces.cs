using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Board_piece
{
    class ChessPieces
    {
        // Animation that respresents the piece
        public Texture2D piecetexture;
        //Position of the chess piece relative to the upper left side of the screen
        public Vector2 Position;
        //State of the piece
        public bool Active;
        public int Width { get { return piecetexture.Width; } }
        public int Height { get { return piecetexture.Height; } }
        public void Initialize(Texture2D texture, Vector2 position)
        {
            piecetexture = texture;
            //Starting position of the piece (around the middle of the screen and to the back)
            Position = position;
            //Chess piece is visible
            Active = true;
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(piecetexture, Position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, 0f);
        }
    }
}
