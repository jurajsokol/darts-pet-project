using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Darts.Controls
{
    /// <summary>
    /// Interakční logika pro DartsTargetBackgroundButton.xaml
    /// </summary>
    public partial class DartsTargetBackgroundButton : UserControl
    {
        private const double _18DegreesInRadians = 0.314159265;
        private const double _9degreesInRadians = 0.157079633;

        public DartsTargetBackgroundButton()
        {
            InitializeComponent();

            double startingPointX = Math.Cos(-_9degreesInRadians) * 200 + 200;
            double startingPointY = Math.Sin(-_9degreesInRadians) * 200 + 200;

            double endPointX = Math.Cos(_9degreesInRadians) * 200 + 200;
            double endPointY = Math.Sin(_9degreesInRadians) * 200 + 200;

            LineSegment firstLineSegment = new LineSegment(new Point(startingPointX, startingPointY), true);
            ArcSegment arcSegment = new ArcSegment(new Point(endPointX, endPointY), new Size(400, 400), 0, false, SweepDirection.Clockwise, false);
            LineSegment secondLineSegment = new LineSegment(new Point(200, 200), true);
        }
    }
}
