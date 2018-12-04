using System;
using System.Diagnostics;


namespace Seihou
{
    class Debugging
    {
		[Conditional("DEBUG")]
        public static void Write(object sender,string message) => Console.WriteLine($"{sender} : {message}");

        [Conditional("DEBUG")]
        public static void Write(Entity e)
        {
            Console.WriteLine($"Entity_{e.ToString()} "+"{");
            Console.WriteLine($"ID: {e}");
            Console.WriteLine($"HP: {e.hp}");
            Console.WriteLine($"POS: X{e.pos.X} , Y{e.pos.Y}");
            Console.WriteLine($"SPEED: X{e.speed.X} , Y{e.speed.Y}");
            Console.WriteLine($"SIZE: {e.size}");
            Console.WriteLine("}");
        }
    }
}
