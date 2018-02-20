using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeorijaGrafovaDz2
{
  public class Graph
  {
    readonly int _verticesCount;
    readonly int[,] _vertexMatrix;
    readonly Dictionary<int, List<int>> _vertexNeighbours;
    public const int ColourCount = 3;
    readonly List<int> _colour;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="file">File path to graph neighbourhood matrix..</param>
    public Graph(String file)
    {
      var reader = new GraphReader(file);
      _vertexMatrix = reader.Read();
      _verticesCount = _vertexMatrix.GetLength(0);

      /* create a dictionary of adjacent peaks of each peak. */
      _vertexNeighbours = new Dictionary<int, List<int>>();
      MarkNeighbours();

      /* Initialize the color list all to a negative number. */
      _colour = new List<int>(_verticesCount);
      for (var i = 0; i < _verticesCount; i++)
      {
        _colour.Add(int.MinValue);
      }

    }

    /// <summary>
    /// Checks if a grapf is a 3-colorable, nonrecursive, using DFP.
    /// </summary>
    /// <returns>true/false, depending if graph is 3-colorable</returns>
    public bool Is3Colourable()
    {
      var result = true;

      var visited = new HashSet<int>();
      var toVisit = new Stack<int>();

      toVisit.Push(0);

      for (var u = 0; u < _verticesCount; u++)
      {
        if (!visited.Contains(u))
        {
          toVisit.Push(u);
          while (toVisit.Count > 0)
          {
            var vertex = toVisit.Pop();
            if (!visited.Contains(u))
            {
              visited.Add(u);

              //* search avaliable color in neighbourhood and if possible add first avaliable.
              for (var c = 0; c < ColourCount; c++)
              {
                if (!IsColorInNeighbours(c, vertex))
                {
                  _colour[vertex] = c;
                }
              }

              foreach (var neighbour in _vertexNeighbours[vertex])
              {
                if (visited.Contains(neighbour))
                {
                  toVisit.Push(neighbour);
                }
              }
            }
          }
        }
      }
      /* if there is any vertice that is not colored, then graph is not colorable. */
      if (_colour.Contains(int.MinValue))
      {
        result = false;
      }

      return result;
    }

    /// <summary>
    /// Check neighbours if they are colored with specific color.
    /// </summary>
    /// <param name="vertexColour">Color for checking</param>
    /// <param name="vertex">vertex for checking its neighbours</param>
    /// <returns></returns>
    private bool IsColorInNeighbours(int vertexColour, int vertex)
    {
      var returnVal = false;
      foreach (var neighbour in _vertexNeighbours[vertex])
      {
        /* ako je boja među susjedima i susjed je ofarban */
        if (vertexColour == _colour[neighbour] && _colour[neighbour] != int.MinValue)
        {
          returnVal = true;
          break;
        }
      }
      return returnVal;
    }

    /// <summary>
    /// Find neighbours and mark them.
    /// </summary>
    private void MarkNeighbours()
    {
      for (var i = 0; i < _verticesCount; i++)
      {
        var neighbours = new List<int>();
        for (var j = 0; j < _verticesCount; j++)
        {
          if (_vertexMatrix[i, j] != 0)
          {
            neighbours.Add(j);
          }
        }
        _vertexNeighbours.Add(i, neighbours);
      }
    }

    public List<int> ValidColour => _colour;
  }
}
