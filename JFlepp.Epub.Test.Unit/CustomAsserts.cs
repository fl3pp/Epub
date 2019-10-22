using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFlepp.Epub.Test.Unit
{
    public static class CustomAsserts
    {
        public static void AssertNavigationPointsAreEqual(IEnumerable<NavigationPoint> points1, IEnumerable<NavigationPoint> points2)
        {
            if (points1.Count() != points2.Count()) throw new AssertFailedException("Collections count is different.");

            for (int i = 0; i < points1.Count(); i++)
            {
                var point1 = points1.ElementAt(i);
                var point2 = points2.ElementAt(i);

                if (point1.Title != point2.Title) throw new AssertFailedException($"Titles don't match: '{point1.Title}' and '{point2.Title}'.");
                if (point1.Order != point2.Order) throw new AssertFailedException($"Orders don't match: '{point1.Order}' and '{point2.Order}'.");
                if (point1.File != point2.File) throw new AssertFailedException($"Files don't match: '{point1.File}' and '{point2.File}'.");
                if (point1.ElementId != point2.ElementId) throw new AssertFailedException($"ElementIds don't match: '{point1.ElementId}' and '{point2.ElementId}'.");
                AssertNavigationPointsAreEqual(point1.Children, point2.Children);
            }
        }
        
    }
}
