using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ChessTests
{
    public static class ReadFromFile
    {
        public static List<Moves> ProcessFile(string path)
        {
            return
                File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .ParseLinesToMoves()
                .ToList();
        }

        private static IEnumerable<Moves> ParseLinesToMoves(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var columns = line.Split(",");

                yield return new Moves
                {
                    BlackMoves = columns[0],
                    WhiteMoves = columns[1]
                };
            }
        }
    }
}
