using CSCore.DMO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhythmGameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RhythmGameLibrary
{
    public class PlayerController : InputHandler
    {
        public Vector2 Direction { get; private set; }
        public Vector2 MousePos;
        public bool IsShooting { get; private set; }
        public bool IsAccelerating { get; private set; }
        int playerNumber;

        public PlayerController(int playerNum)
        {
            playerNumber= playerNum;
        }
        public override void Update()
        {
            base.Update();
            setDirection(playerNumber);
            checkInputs(playerNumber);
            setMousePos();
        }

        private void setDirection(int playerNum)
        {
            if (isUsingGamepad())
            {
                Direction = Thumbstick(playerNum);
            }
            else
            {
                
                Direction = KeyboardDirection(playerNum);
            }
        }

        private void setMousePos()
        {
            MousePos = Mouse.GetState().Position.ToVector2();
        }

        private void checkInputs(int playerNum)
        {
            switch (playerNum)
            {
                case 0:
                    if (isUsingGamepad())
                    {
                        IsShooting = IsButtonPressed(Microsoft.Xna.Framework.Input.Buttons.X);
                    }
                    else
                    {
                        IsShooting = IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter);
                    }
                    if (isUsingGamepad())
                    {
                        IsAccelerating = IsButtonPressed(Microsoft.Xna.Framework.Input.Buttons.A);
                    }
                    else
                    {
                        IsAccelerating = IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space);
                    }
                    break;
                    //need to change this prbobly 
                case 1:
                    if (isUsingGamepad())
                    {
                        IsShooting = IsButtonPressed(Microsoft.Xna.Framework.Input.Buttons.X);
                    }
                    else
                    {
                        IsShooting = IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.X);
                    }
                    if (isUsingGamepad())
                    {
                        IsAccelerating = IsButtonPressed(Microsoft.Xna.Framework.Input.Buttons.A);
                    }
                    else
                    {
                        IsAccelerating = IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space);
                    }
                    break;
            }
            
        }


    }
}
