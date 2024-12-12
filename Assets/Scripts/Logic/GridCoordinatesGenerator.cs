using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic
{
    public class GridCoordinatesGenerator
    {
        private const int INITIAL_POINTS = 3;

        private int _rows;
        private float _rowSpacing;
        private float _pointSpacing;

        public GridCoordinatesGenerator(int rows, float rowSpacing, float pointSpacing)
        {
            _rows = rows;
            _rowSpacing = rowSpacing;
            _pointSpacing = pointSpacing;
        }

        public List<Vector2> GenerateGrid()
        {
            List<Vector2> points = new List<Vector2>();

            for (int row = 0; row < _rows; row++)
            {
                int pointsInRow = INITIAL_POINTS + row;
                float offset = -(pointsInRow - 1) * _pointSpacing / 2;

                for (int col = 0; col < pointsInRow; col++)
                {
                    float x = col * _pointSpacing + offset;
                    float y = row * _rowSpacing * -1;
                    points.Add(new Vector2(x, y));
                }
            }

            return points;
        }
    }
}