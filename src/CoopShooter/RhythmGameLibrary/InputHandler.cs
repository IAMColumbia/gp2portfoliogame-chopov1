﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace RhythmGameLibrary
{
    public class InputHandler
    {
        private KeyboardState KeyboardState;
        KeyboardState prevKeyboardState;
        GamePadState GamePadState;
        GamePadState prevGamePadState;
        public Dictionary<string, Keys[]> inputKeys;
        public Dictionary<string, Buttons[]> inputButtons;
        public string[] stickDirs;

        public InputHandler()
        {
            setupKeyDic();
            setupButtonDic();
            setupStickDirs();
        }
        private void setupButtonDic()
        {
            inputButtons = new Dictionary<string, Buttons[]>();
            inputButtons.Add("Up", new Buttons[] { Buttons.A, Buttons.X });
        }
        private void setupKeyDic()
        {
            inputKeys = new Dictionary<string, Keys[]>();
            inputKeys.Add("Right", new Keys[] { Keys.Right, Keys.D });
            inputKeys.Add("Left", new Keys[] { Keys.Left, Keys.A });
            inputKeys.Add("Up", new Keys[] { Keys.Up, Keys.W });
            inputKeys.Add("Down", new Keys[] { Keys.Down, Keys.S });
        }
        private void setupStickDirs()
        {
            stickDirs = new string[4];
            stickDirs[0] = "Right";
            stickDirs[1] = "Left";
            stickDirs[2] = "Down";
            stickDirs[3] = "Up";
        }
        public virtual void Update()
        {
            prevKeyboardState = KeyboardState;
            prevGamePadState = GamePadState;
            KeyboardState = Keyboard.GetState();
            GamePadState = GamePad.GetState(0);
        }

        public bool isUsingGamepad()
        {
            return GamePad.GetState(0).IsConnected;
        }

        public Vector2 Thumbstick(int player)
        {
            return GamePad.GetState(player).ThumbSticks.Left;
        }

        bool pressedDir;
        Vector2 stickValue;

        public Vector2 KeyboardDirection(int playerNum)
        {
            Vector2 dir = new Vector2(0, 0);
            switch (playerNum)
            {
                default:
                    if (IsKeyPressed(Keys.Right)) { dir += new Vector2(1, 0); }
                    if (IsKeyPressed(Keys.Left)) { dir += new Vector2(-1, 0); }
                    if (IsKeyPressed(Keys.Up)) { dir += new Vector2(0, -1); }
                    if (IsKeyPressed(Keys.Down)) { dir += new Vector2(0, 1); }
                    return dir;
                case 1:
                    if (IsKeyPressed(Keys.D)) { dir += new Vector2(1, 0); }
                    if (IsKeyPressed(Keys.A)) { dir += new Vector2(-1, 0); }
                    if (IsKeyPressed(Keys.W)) { dir += new Vector2(0, -1); }
                    if (IsKeyPressed(Keys.S)) { dir += new Vector2(0, 1); }
                    return dir;
            }

        }
        public bool IsDirectionDown(string dir, int playerNum)
        {
            stickValue = Thumbstick(playerNum);
            if (!pressedDir)
            {
                switch (dir)
                {
                    case "Right":
                        if (stickValue.X >= .6 && stickValue.X > Math.Abs(stickValue.Y))
                        {
                            pressedDir = true;
                            return true;
                        }
                        break;
                    case "Left":
                        if (stickValue.X <= -.6 && stickValue.X < -1 * Math.Abs(stickValue.Y))
                        {
                            pressedDir = true;
                            return true;
                        }
                        break;
                    case "Down":
                        if (stickValue.Y >= .6 && stickValue.Y > Math.Abs(stickValue.X))
                        {
                            pressedDir = true;
                            return true;
                        }
                        break;
                    case "Up":
                        if (stickValue.Y <= -.6 && stickValue.Y < -1 * Math.Abs(stickValue.X))
                        {
                            pressedDir = true;
                            return true;
                        }
                        break;
                }
            }
            else if (stickValue == Vector2.Zero)
            {
                pressedDir = false;
            }
            return false;
        }

        public bool IsDirectionPressed(string dir, int playerNum)
        {
            Vector2 stickValue = Thumbstick(playerNum);
            switch (dir)
            {
                case "Right":
                    if (stickValue.X >= .6 && stickValue.X > Math.Abs(stickValue.Y))
                    {
                        return true;
                    }
                    break;
                case "Left":
                    if (stickValue.X <= -.6 && stickValue.X < -1 * Math.Abs(stickValue.Y))
                    {
                        return true;
                    }
                    break;
                case "Down":
                    if (stickValue.Y >= .6 && stickValue.Y > Math.Abs(stickValue.X))
                    {
                        return true;
                    }
                    break;
                case "Up":
                    if (stickValue.Y <= -.6 && stickValue.Y < -1 * Math.Abs(stickValue.X))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public bool IsButtonPressed(Buttons button)
        {
            return GamePadState.IsButtonDown(button);
        }

        public bool IsHoldingButton(Buttons button)
        {
            if (prevGamePadState.IsButtonDown(button) && GamePadState.IsButtonDown(button))
            {
                return true;
            }
            return false;
        }

        public bool ReleasedButton(Buttons button)
        {
            if (prevGamePadState.IsButtonDown(button) && !GamePadState.IsButtonDown(button))
            {
                return true;
            }
            return false;
        }

        public bool IsKeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        public bool PressedKey(Keys key)
        {
            if (!prevKeyboardState.IsKeyDown(key) && KeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool releasedAllKeys(List<Keys[]> keys, int playerNum)
        {
            foreach (var key in keys)
            {
                if (PressedKey(key[playerNum]))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsHoldingKey(Keys key)
        {
            if (prevKeyboardState.IsKeyDown(key) && KeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool ReleasedKey(Keys key)
        {
            if (prevKeyboardState.IsKeyDown(key) && !KeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool IsPressingAnyKey()
        {
            if (KeyboardState.GetPressedKeyCount() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
