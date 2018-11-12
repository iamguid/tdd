using System.Collections.Generic;
using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private static readonly Size InitialQuadTreeHalfSize = new Size(500, 500);
        private const int SpiralSize = 10;
        private double _currentAngle = Math.PI;
        private Point _currentPoint;
        private Point _layoutCenter;
        private QuadTree<Rectangle> _rectanglesQuadTree;

        public CircularCloudLayouter(Point center)
        {
            _layoutCenter = center;
            _currentPoint = center;
            
            _rectanglesQuadTree = new QuadTree<Rectangle>(
                new Rectangle(
                    _layoutCenter.X - InitialQuadTreeHalfSize.Width,
                    _layoutCenter.Y - InitialQuadTreeHalfSize.Height,
                    InitialQuadTreeHalfSize.Width * 2,
                    InitialQuadTreeHalfSize.Height * 2));
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var nextRect = new Rectangle(_currentPoint, rectangleSize);

            _currentAngle = Math.PI;
            while (_rectanglesQuadTree.HasNodesInside(nextRect))
            {
                var r = SpiralSize * _currentAngle;
                var x = (int)Math.Floor(r * Math.Cos(_currentAngle));
                var y = (int)Math.Floor(r * Math.Sin(_currentAngle));
                
                _currentPoint = new Point(x, y);
                _currentAngle += 0.1;
                
                nextRect = new Rectangle(_currentPoint, rectangleSize);
            }
            
            _rectanglesQuadTree.Insert(nextRect, nextRect);
            
            return nextRect;
        }

        public IEnumerable<Rectangle> GetLayout()
        {
            return _rectanglesQuadTree.Nodes;
        }
    }
}