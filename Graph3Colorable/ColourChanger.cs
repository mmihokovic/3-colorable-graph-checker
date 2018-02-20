using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeorijaGrafovaDz2
{
    class ColourChanger
    {
        int verticesCount;
        List<int> colourPermutation;
        int colourCount;
        int firstFixedVertex;
        int secondFixedVertex;

        public ColourChanger(int verticesCount, int colourCount, int firstFixedVertex, int secondFixedVertex)
        {
            this.verticesCount = verticesCount;
            this.colourCount = colourCount;
            this.firstFixedVertex = firstFixedVertex;
            this.secondFixedVertex = secondFixedVertex;
            colourPermutation = new List<int>();
            for (var i = 0; i < this.verticesCount; i++)
            {
                colourPermutation.Add(0); //ili random
            }
            colourPermutation.RemoveAt(firstFixedVertex);
            colourPermutation.RemoveAt(secondFixedVertex);
        }

        public List<int> Next()
        {
            var overflow = false;

            colourPermutation[0] += 1;

            for (var i = 0; i < verticesCount; i++)
            {
                if (overflow)
                {
                    colourPermutation[i] += 1;
                    overflow = false;
                }

                if (colourPermutation[i] >= colourCount)
                {
                    colourPermutation[i] = colourPermutation[i] % colourCount;
                    overflow = true;
                }
                else
                {
                    return colourPermutation;
                }

            }

            return colourPermutation;
        }


    }
}
