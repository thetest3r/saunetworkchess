using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NetworkChess;

namespace Board_piece
{
    class ChessPieces
    {
        // Animation that respresents the piece
        //public Texture2D piecetexture;
        public Animation pieceAnimation;

        //Position of the chess piece relative to the upper left side of the screen
        public Vector2 Position;
        //State of the piece
        public bool Active;
        public int Health;
        //public int Width { get { return piecetexture.Width; } }
        //public int Height { get { return piecetexture.Height; } }
        /*public void Initialize(Texture2D texture, Vector2 position)
        {
            piecetexture = texture;
            //Starting position of the piece (around the middle of the screen and to the back)
            Position = position;
            //Chess piece is visible
            Active = true;
        }*/

        public int Width { get { return pieceAnimation.FrameWidth; } }
        public int Height { get { return pieceAnimation.FrameHeight; } }
        public void Initialize(Animation animation, Vector2 position)
        {
            pieceAnimation = animation;            
            //Set the starting positoin of the player around the middle of the screen and to the back
            Position = position;
            //Set the player to be active
            Active = true;
            //Set the player heatlh
            Health = 100;
        }
        public void Update(GameTime gametime)
        {
            pieceAnimation.Position = Position;
            pieceAnimation.Update(gametime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            pieceAnimation.Draw(spriteBatch);
        }
        /*public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(piecetexture, Position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, 0f);
        }*/
    }
}
