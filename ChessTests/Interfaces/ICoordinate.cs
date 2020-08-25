using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests.Interfaces
{
    public interface ICoordinate
    {
        Coordinate Coordinate { get;}
        string Coordinates { get; }
        int Y { get;}
    }
}
