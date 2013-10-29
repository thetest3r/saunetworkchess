using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NetworkChess
{
    class ParallaxBackground
    {
        Texture2D texture;
        Vector2[] positions;
        int speed;
        public void Initialize(ContentManager content, String texturePath, int screenWidth, int screenHeight, int speed)
        {
            bgHeight = screenHeight;
            bgWidth = screenWidth;
        }
        public void Update()
        {

        }
        public void Draw()
        {

        }
    }
}
