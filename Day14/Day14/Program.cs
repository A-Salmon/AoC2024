using System;

namespace DayX
{
    class DayX
    {
        public static void Main(string[] args)
        {
            var path = "../../../../puzzle.txt";
            var lines = File.ReadAllLines(path);

            var robots = new List<Robot>();

            foreach (var line in lines)
            {
                (string pos, string vel) = (line.Split(' ')[0], line.Split(' ')[1]);

                var position = pos[2..].Split(',');
                var velocity = vel[2..].Split(',');
                
                robots.Add(new Robot((Int32.Parse(position[0]), Int32.Parse(position[1])), (Int32.Parse(velocity[0]), Int32.Parse(velocity[1]))));
            }

            int steps = 10000;
            int width = 101;
            int height = 103;


            for (int i = 0; i < steps; i++)
            {
                var field = new int[height][];
                for(int j = 0; j < field.Length; j++)
                {
                    field[j] = new int[width];
                }
                
                foreach (var robot in robots)
                {
                    robot.Position = ((robot.Position.X + robot.Velocity.X + width) % width,
                        (robot.Position.Y + robot.Velocity.Y + height) % height);
                }

                foreach (var robot in robots)
                {
                    field[robot.Position.Y][robot.Position.X]++;
                }

                foreach (var row in field)
                {
                    var str = "";
                    foreach (var c in row)
                    {
                        str += c == 0 ? "." : c;
                    }

                    if ((i - 12) % 101 == 0)
                    {
                        Console.WriteLine(str);
                    }
                    
                }
                if ((i - 12) % 101 == 0)
                {
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("t = " + (i + 1));
                }

            }
            

            
            int result = 0;
            var quadrant = new int[][]
            {
                [0, 0],
                [0, 0]
            };
            
            foreach (var robot in robots)
            {
                //Console.WriteLine(robot.Position);
                if (robot.Position.X == width / 2 || robot.Position.Y == height / 2) continue;
                var y = (int) (robot.Position.Y / ((float)height / 2));
                var x = (int) (robot.Position.X / ((float)width / 2));
                quadrant[y][x]++;
            }

            result = quadrant[0][0] * quadrant[0][1] * quadrant[1][0] * quadrant[1][1];

            Console.WriteLine("-> " + result + " <-");
            

            

        }
    }

    class Robot((int X, int Y) position, (int X, int Y) velocity)
    {
        public (int X, int Y) Position { get; set; } = position;
        public (int X, int Y) Velocity { get; } = velocity;

        public override bool Equals(object? obj)
        {
            return obj != null && ((Robot) obj).Equals(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Velocity);
        }

        private bool Equals(Robot other)
        {
            return Position.Equals(other.Position) && Velocity.Equals(other.Velocity);
        }

        public override string ToString()
        {
            return $"{Position}{Velocity}";
        }
    }
}