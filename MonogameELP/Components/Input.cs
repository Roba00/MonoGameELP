using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonogameELP.Components
{
    public static class Input
    {
        private static KeyboardState currentKeyState;
        private static KeyboardState previousKeyState;
        //private static int frames = 0;
        //private static int frameReset = 120;

        public static KeyboardState UpdateState()
        {
            //if (frames == frameReset)
            //{
                previousKeyState = currentKeyState;
            //    frames = 0;
            //}
            //frames++;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        public static bool GetLeft()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Left);
        }

        public static bool GetRight()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Right);
        }

        public static bool GetUp()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Up);
        }

        public static bool GetZ()
        {
            return currentKeyState.IsKeyDown(Keys.Z);
        }

        public static bool GetZ_Down()
        {
            return currentKeyState.IsKeyDown(Keys.Z) && !previousKeyState.IsKeyDown(Keys.Z);
        }

        public static bool GetX()
        {
            return currentKeyState.IsKeyDown(Keys.X);
        }

        public static bool GetX_Down()
        {
            return currentKeyState.IsKeyDown(Keys.X) && !previousKeyState.IsKeyDown(Keys.X);
        }

        public static bool GetX_Up()
        {
            return Keyboard.GetState().IsKeyUp(Keys.X);
        }
    }
}
