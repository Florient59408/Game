using System;
using System.Collections.Generic;
using System.Drawing;

namespace SnakeGame
{
    enum Directions
    {
        Left,
        Right,
        Up,
        Down
    };
    class Snake
    {
        public Directions way = Directions.Down;
        public int Lenght { get; set; }
        public List<Circle> Body { get; set; }

        public Circle Food { get; set; }
        public Snake(Circle head, Size gameArea)
        {
            Body = new List<Circle>();
            Body.Add(head);
            Lenght = Body.Count;
            Food = GenerateFood(gameArea);
        }

        public bool MoveSnake(Size gameArea)
        {
            for(int i = this.Body.Count -1; i>=0; i--)
            {
                if(i == 0)
                {
                    switch(way)
                    {
                        case Directions.Left:
                            this.Body[i].X -= 1;
                            break;
                        case Directions.Right:
                            this.Body[i].X += 1;
                            break;
                        case Directions.Up:
                            this.Body[i].Y -= 1;
                            break;
                        case Directions.Down:
                            this.Body[i].Y += 1;
                            break;
                        default:
                            break;
                    }

                    //Collision with game area
                    if (this.Body[i].X > gameArea.Width/Settings.Width || this.Body[i].X < 0)
                        return false;
                    else if (this.Body[i].Y > gameArea.Height/Settings.Height || this.Body[i].Y < 0)
                        return false;

                    //Collision with another part of body
                    for(int j = 1; j<this.Body.Count; j++)
                    {
                        if ( this.Body[i].X == this.Body[j].X && this.Body[i].Y == this.Body[j].Y)
                            return false;
                    }

                    //The snake ate the food
                    if (this.Body[i].X == Food.X && this.Body[i].Y == Food.Y)
                    {
                        EatFood(gameArea);
                        continue;
                    }
                }

                //Move the rest of the body
                else
                {
                    this.Body[i].X = this.Body[i - 1].X;
                    this.Body[i].Y = this.Body[i - 1].Y;
                    continue;
                }
            }
            return true;
        }

        private void EatFood(Size gameArea)
        {
            Circle newPart = newPart = new Circle(this.Body[Body.Count - 1].X, this.Body[Body.Count - 1].Y);
            this.Body.Add(newPart);
            this.Lenght = this.Body.Count;
            this.Food = GenerateFood(gameArea);
        }

        private Circle GenerateFood(Size gameArea)
        {
            Random rand = new Random();
            return new Circle(rand.Next(gameArea.Width/Settings.Width), rand.Next(gameArea.Height/Settings.Height));
        }
    }
}
