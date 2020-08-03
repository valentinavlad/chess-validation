using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChessTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = args[0];
            List<string> listOfMoves = ReadFromFile.ProcessFile(path);
            
            //var game = new Game();
            //try
            //{
            //    game.Play(listOfMoves);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
          
    
        }
    }
}
