using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Demo.Controls
{
    public class TiledBackground : ContentControl
    {
        private readonly Image _tiledImage = new Image();
        private BitmapImage _bitmap;
        private WriteableBitmap _sourceBitmap;
        private int _lastWidth, _lastHeight;

        public TiledBackground()
        {
            // create an image as the content of the control
            _tiledImage.Stretch = Stretch.None;
            Content = _tiledImage;

            // no SizeChanged to override
            SizeChanged += TiledBackgroundSizeChanged;
        }

        private void TiledBackgroundSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTiledImage();
        }

        private void UpdateTiledImage()
        {
            if (_sourceBitmap == null)
            {
                return;
            }

            var width = (int)Math.Ceiling(ActualWidth);
            var height = (int)Math.Ceiling(ActualHeight);

            // only regenerate the image if the width/height has grown
            if (width < _lastWidth && height < _lastHeight)
            {
                return;
            }

            _lastWidth = width;
            _lastHeight = height;

            var final = new WriteableBitmap(width, height);

            for (int x = 0; x < final.PixelWidth; x++)
            {
                for (int y = 0; y < final.PixelHeight; y++)
                {
                    int tiledX = (x % _sourceBitmap.PixelWidth);
                    int tiledY = (y % _sourceBitmap.PixelHeight);
                    final.Pixels[y * final.PixelWidth + x] = _sourceBitmap.Pixels[tiledY * _sourceBitmap.PixelWidth + tiledX];
                }
            }

            _tiledImage.Source = final;
        }

        #region SourceUri (DependencyProperty)

        public Uri SourceUri
        {
            get { return (Uri)GetValue(SourceUriProperty); }
            set { SetValue(SourceUriProperty, value); }
        }

        public static readonly DependencyProperty SourceUriProperty =
             DependencyProperty.Register("SourceUri", typeof(Uri), typeof(TiledBackground),
             new PropertyMetadata(null, OnSourceUriChanged));

        private static void OnSourceUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TiledBackground)d).OnSourceUriChanged(e);
        }

        protected virtual void OnSourceUriChanged(DependencyPropertyChangedEventArgs e)
        {
            _bitmap = new BitmapImage(e.NewValue as Uri) { CreateOptions = BitmapCreateOptions.None };
            _bitmap.ImageOpened += BitmapImageOpened;
        }

        private void BitmapImageOpened(object sender, RoutedEventArgs e)
        {
            _sourceBitmap = new WriteableBitmap(_bitmap);
            UpdateTiledImage();
        }

        #endregion
    }
}
