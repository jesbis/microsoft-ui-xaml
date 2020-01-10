// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Markup;
using Windows.UI;
using System.Windows.Input;
using System.Numerics;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.Foundation.Metadata;

#if !BUILD_WINDOWS
using RadialGradientBrush = Microsoft.UI.Xaml.Media.RadialGradientBrush;
#endif

namespace MUXControlsTestApp
{
    [TopLevelTestPage(Name = "RadialGradientBrush")]
    public sealed partial class RadialGradientBrushPage : TestPage
    {
        public RadialGradientBrush DynamicGradientBrush;
        private Random _random;

        public RadialGradientBrushPage()
        {
            this.InitializeComponent();
            _random = new Random();
        }

        public void ReplaceGradientButton_Click(object sender, RoutedEventArgs e)
        {
            DynamicGradientBrush = new RadialGradientBrush();
            DynamicGradientBrush.FallbackColor = Color.FromArgb(Byte.MaxValue, (byte)_random.Next(256), (byte)_random.Next(256), (byte)_random.Next(256));

            // Set brush before adding stops
            RectangleWithDynamicGradient.Fill = DynamicGradientBrush;

            DynamicGradientBrush.GradientStops.Clear();
            for (int i = 0; i < _random.Next(2, 5); i++)
            {
                AddRandomGradientStop(DynamicGradientBrush);
            }

            // Set brush after adding stops
            TextBlockWithDynamicGradient.Foreground = DynamicGradientBrush;

            Bindings.Update();
        }

        public void AddGradientStopButton_Click(object sender, RoutedEventArgs e)
        {
            AddRandomGradientStop(DynamicGradientBrush);
        }

        public void RemoveGradientStopButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveRandomGradientStop(DynamicGradientBrush);
        }

        public void RandomizeGradientOriginButton_Click(object sender, RoutedEventArgs e)
        {
            RandomizeGradientOriginOffset(DynamicGradientBrush);
        }

        public void RandomizeEllipseCenterButton_Click(object sender, RoutedEventArgs e)
        {
            RandomizeEllipseCenter(DynamicGradientBrush);
        }

        public void RandomizeEllipseRadiusButton_Click(object sender, RoutedEventArgs e)
        {
            RandomizeEllipseRadius(DynamicGradientBrush);
        }

        public void ToggleMappingModeButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleMappingMode(DynamicGradientBrush);
        }

        public async void GenerateRenderTargetBitmapButton_Click(object sender, RoutedEventArgs e)
        {
            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(GradientRectangle);

            RenderTargetBitmapResultRectangle.Fill = new ImageBrush() { ImageSource = rtb };

            var pixelBuffer = await rtb.GetPixelsAsync();
            byte[] pixelArray = pixelBuffer.ToArray();

            // Sample top left and center pixels to verify rendering is correct.
            var centerColor = GetPixelAtPoint(new Point(50, 50), rtb, pixelArray);
            var outerColor = GetPixelAtPoint(new Point(0, 0), rtb, pixelArray);

            if (ApiInformation.IsTypePresent("Windows.UI.Composition.CompositionRadialGradientBrush"))
            {
                // If CompositionRadialGradientBrush is available then should be rendering a gradient.
                if (centerColor == Colors.Orange && outerColor == Colors.Green)
                {
                    ColorMatchTestResult.Text = "Passed";
                }
                else
                {
                    ColorMatchTestResult.Text = "Failed";
                }
            }
            else
            {
                if (centerColor == Colors.Red && outerColor == Colors.Red)
                {
                    ColorMatchTestResult.Text = "Passed";
                }
                else
                {
                    ColorMatchTestResult.Text = "Failed";
                }
            }
        }

        private void AddRandomGradientStop(RadialGradientBrush gradientBrush)
        {
            if (gradientBrush != null)
            {
                var stop = new GradientStop();
                stop.Color = Color.FromArgb(Byte.MaxValue, (byte)_random.Next(256), (byte)_random.Next(256), (byte)_random.Next(256));
                stop.Offset = _random.NextDouble();

                gradientBrush.GradientStops.Add(stop);
            }
        }

        private void RemoveRandomGradientStop(RadialGradientBrush gradientBrush)
        {
            if (gradientBrush != null && gradientBrush.GradientStops.Count > 0)
            {
                gradientBrush.GradientStops.RemoveAt(_random.Next(0, gradientBrush.GradientStops.Count - 1));
            }
        }

        private void RandomizeGradientOriginOffset(RadialGradientBrush gradientBrush)
        {
            if (gradientBrush != null)
            {
                if (gradientBrush.MappingMode == BrushMappingMode.Absolute)
                {
                    gradientBrush.GradientOriginOffset = new Vector2((float)_random.Next(0, 100), (float)_random.Next(0, 100));
                }
                else
                {
                    gradientBrush.GradientOriginOffset = new Vector2(_random.Next(-100, 100) / 100f, _random.Next(-100, 100) / 100f);
                }
            }
        }

        private void RandomizeEllipseCenter(RadialGradientBrush gradientBrush)
        {
            if (gradientBrush != null)
            {
                if (gradientBrush.MappingMode == BrushMappingMode.Absolute)
                {
                    gradientBrush.EllipseCenter = new Vector2((float)_random.Next(0, 100), (float)_random.Next(0, 100));
                }
                else
                {
                    gradientBrush.EllipseCenter = new Vector2((float)_random.NextDouble(), (float)_random.NextDouble());
                }
            }
        }

        private void RandomizeEllipseRadius(RadialGradientBrush gradientBrush)
        {
            if (gradientBrush != null)
            {
                if (gradientBrush.MappingMode == BrushMappingMode.Absolute)
                {
                    gradientBrush.EllipseRadius = new Vector2((float)_random.Next(10, 200), (float)_random.Next(10, 200));
                }
                else
                {
                    gradientBrush.EllipseRadius = new Vector2((float)_random.NextDouble(), (float)_random.NextDouble());
                }
            }
        }

        private void ToggleMappingMode(RadialGradientBrush gradientBrush)
        {
            if (gradientBrush != null)
            {
                gradientBrush.MappingMode = ((gradientBrush.MappingMode == BrushMappingMode.RelativeToBoundingBox) ? BrushMappingMode.Absolute : BrushMappingMode.RelativeToBoundingBox);

                RandomizeEllipseCenter(gradientBrush);
                RandomizeEllipseRadius(gradientBrush);
                RandomizeGradientOriginOffset(gradientBrush);
            }
        }

        private Color GetPixelAtPoint(Point p, RenderTargetBitmap rtb, byte[] pixelArray)
        {
            Color pixelColor = new Color();
            int x = (int)(p.X);
            int y = (int)(p.Y);
            int pointPosition = 4 * (rtb.PixelWidth * y + x);

            pixelColor.B = pixelArray[pointPosition];
            pixelColor.G = pixelArray[pointPosition + 1];
            pixelColor.R = pixelArray[pointPosition + 2];
            pixelColor.A = pixelArray[pointPosition + 3];

            return pixelColor;
        }
    }
}
