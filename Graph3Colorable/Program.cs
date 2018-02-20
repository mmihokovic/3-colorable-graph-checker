using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace TeorijaGrafovaDz2
{
  class Program
  {
    static void Main(string[] args)
    {
      var timer = new Stopwatch();
      timer.Start();
      var graph = new Graph(args[0]);
      var colorable = graph.Is3Colourable();
      timer.Stop();
      Console.WriteLine(colorable ? "Graph is 3-colorable" : "Graph isn't 3 - colorable");



      if (colorable)
      {
        var colour = graph.ValidColour;
        for (int i = 0; i < colour.Capacity; i++)
        {
          Console.WriteLine("Vrh: " + i + " Boja: " + colour[i]);
        }
      }
      Console.WriteLine("Elapsed time: " + timer.ElapsedMilliseconds + " ms");
      Console.WriteLine("Press 'Enter' to quit");
      Console.Read();
    }
  }
}
