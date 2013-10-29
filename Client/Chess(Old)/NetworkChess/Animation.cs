using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NetworkChess
{
    class Animation
    {
        //The image representing the collection of images used for animation
        Texture2D spriteStrip;
        //The scaler used to display the sprite strip
        float scale;
        //The time since we last updated the frame
        int elapsedTime;
        //The time we display a frame until the next one
        int frameTime;
        //The number of frames that the animation contains
        int frameCount;
        //The index of the current frame we are displaying
        int currentFrame;
        //The color of the frame we will be displaying
        Color color;
        //The area of the image strip we want to display
        Rectangle sourceRect = new Rectangle();
        //The area where we want to display the image strip in the game
        Rectangle destinationRect = new Rectangle();
        //Width of a given frame
        public int FrameWidth;
        //Height of a given frame
        public int FrameHeight;
        //The state of the animation
        public bool Active;
        //Determines if the animation will keep playing or dactivate after one run
        public bool Looping;
        //With of a given frame
        public Vector2 Position;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight,
            int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            //Keep a local copy of the values passed in
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;

            Looping = looping;
            Position = position;
            spriteStrip = texture;

            //Set the time to 0
            elapsedTime = 0;
            currentFrame = 0;

            //Set the Animation to active by default
            Active = true;
        }

        public void Update(GameTime gametime)
        {
            //Do not update if the user is not active
            if (Active == false) return;

            //Update the elapsed
            elapsedTime += (int)gametime.ElapsedGameTime.TotalMilliseconds;
            //If elapsed time is larger than the frame time, we need to switch frames
            if (elapsedTime > frameTime)
            {
                //Move to next frame
                currentFrame++;

                //If the currentFrame is equal to framCount reset currentFrame to zero
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    //If we are nto looping deactivate the aimation
                    if (Looping == false)
                        Active = false;
                }
                //Reset elapsed time to zero
                elapsedTime = 0;
            }
            //Grab the correct frame in the image strip by multiplying the 
            //currentFrame index by the  frame width
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth*scale)/2,
                (int)Position.Y - (int)(FrameHeight * scale)/2, (int) (FrameWidth*scale),
                (int)(FrameHeight*scale));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
        }
    }
}
