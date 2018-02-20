using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TeorijaGrafovaDz2
{
  public class GraphReader
  {
    private readonly StreamReader _file;

    public GraphReader(String path)
    {
      try
      {
        _file = new StreamReader(path);
      }
      catch (FileNotFoundException)
      {
        Console.WriteLine("Error opening: " + path);
      }
    }

    public int[,] Read()
    {
      var vertricesCount = int.Parse(_file.ReadLine());
      var vertexMatrix = new int[vertricesCount, vertricesCount];

      for (var i = 0; i < vertricesCount; i++)
      {
        var line = _file.ReadLine();
        if (line == null || line.Equals("")) /* ako je pročitan prazan red*/
        {
          i--;
          continue;
        }

        var numbers = line.Split(' ');

        for (var j = 0; j < vertricesCount; j++)
        {
          vertexMatrix[i, j] = int.Parse(numbers[j]);
        }
      }

      return vertexMatrix;
    }
  }
}
