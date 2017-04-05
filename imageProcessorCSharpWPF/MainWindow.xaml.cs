using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageProcessorCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Windows.Media.Imaging.BitmapImage Reserve;
        public System.Drawing.Bitmap TransformImage;
        double boxWidth;
        double boxHeight;

        public MainWindow()
        {
            InitializeComponent();



            TG.Children.Add(new TranslateTransform(0.5, 0.5));
            TG.Children.Add(new ScaleTransform(0.5, 0.5));
            imageBox.RenderTransform = TG;
            boxWidth = imageBox.RenderSize.Width;
            boxHeight = imageBox.RenderSize.Height;
            
        }
        
        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            byte[] byteArray = new byte[0];
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Close();
                byteArray = stream.ToArray();
            }
            return byteArray;

        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "Image Files | *.png; *.PNG; *.bmp; *.BMP; *.jpg; *.JPG";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                try
                {
                    System.Windows.Media.Imaging.BitmapImage bmpImage1 = new BitmapImage();
                    System.Drawing.Bitmap image1 = AForge.Imaging.Image.FromFile(ofd.FileName);
                    bmpImage1 = Bmp2BmpImage(image1);
                    imageBox.Source = bmpImage1;
                    TransformImage = image1;

                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }
            }
        }
        
        private System.Drawing.Bitmap BmpImage2Bmp(System.Windows.Media.Imaging.BitmapImage bmpImage)
        {

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                System.Windows.Media.Imaging.BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bmpImage));
                enc.Save(stream);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(stream);
                return new System.Drawing.Bitmap(bmp);
            }


        }

        private System.Windows.Media.Imaging.BitmapImage Bmp2BmpImage(System.Drawing.Bitmap bmp)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                System.Windows.Media.Imaging.BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.StreamSource = stream;
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.EndInit();
                return bmpImage;
            }
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Media.Imaging.BitmapImage imagesource = imageBox.Source as System.Windows.Media.Imaging.BitmapImage;
                if (imageBox.Source == null)
                {
                    System.Windows.MessageBox.Show("Add your image to the Image 1 box!");
                    return;
                }


                System.Drawing.Bitmap image = BmpImage2Bmp(imagesource);
                System.Windows.Media.Imaging.BitmapImage final = new System.Windows.Media.Imaging.BitmapImage();

                if (!AForge.Imaging.Image.IsGrayscale(image))
                {
                    AForge.Imaging.Filters.ExtractChannel Grayer = new AForge.Imaging.Filters.ExtractChannel(0);
                    image = Grayer.Apply(image);
                }

                if (Threshold.IsChecked == true)
                {
                    imageBox.Source = null;
                    AForge.Imaging.Filters.Threshold threshold = new AForge.Imaging.Filters.Threshold((int)slider.Value);
                    threshold.ApplyInPlace(image);
                    final = Bmp2BmpImage(image);
                }
                else if (GaussianFilter.IsChecked == true)
                {


                    AForge.Imaging.Filters.GaussianBlur Gauss = new AForge.Imaging.Filters.GaussianBlur();
                    Gauss.Sigma = GaussSigma_Slide.Value;
                    Gauss.Size = (int)GaussSize_Slide.Value;
                    AForge.Imaging.UnmanagedImage unmanagedImage = AForge.Imaging.UnmanagedImage.FromManagedImage(image);
                    AForge.Imaging.UnmanagedImage Dst = unmanagedImage.Clone();
                    Gauss.Apply(unmanagedImage, Dst);
                    final = Bmp2BmpImage(Dst.ToManagedImage());
                }

                else if (HiPass.IsChecked == true)
                {
                    AForge.Imaging.Filters.Sharpen filter = new AForge.Imaging.Filters.Sharpen();
                    filter.ApplyInPlace(image);
                    final = Bmp2BmpImage(image);
                }

                else if (Erode.IsChecked == true)
                {
                    AForge.Imaging.Filters.Erosion filter = new AForge.Imaging.Filters.Erosion();
                    filter.ApplyInPlace(image);
                    final = Bmp2BmpImage(image);
                }

                else if (Invert.IsChecked == true)
                {
                    AForge.Imaging.Filters.Invert filter = new AForge.Imaging.Filters.Invert();
                    filter.ApplyInPlace(image);
                    final = Bmp2BmpImage(image);
                }
                else if (EdgeDetector.IsChecked == true)
                {
                    AForge.Imaging.Filters.CannyEdgeDetector filter = new AForge.Imaging.Filters.CannyEdgeDetector();
                    filter.ApplyInPlace(image);
                    final = Bmp2BmpImage(image);
                }

                else if (Median.IsChecked == true)
                {
                    AForge.Imaging.Filters.Median filter = new AForge.Imaging.Filters.Median();
                    filter.Size = (int)GaussSize_Slide.Value;
                    filter.ApplyInPlace(image);
                    final = Bmp2BmpImage(image);
                }

                else if (More.IsChecked == true)
                {
                    if (Dilate.IsSelected)
                    {
                        AForge.Imaging.Filters.Dilatation filter = new AForge.Imaging.Filters.Dilatation();
                        filter.ApplyInPlace(image);
                        final = Bmp2BmpImage(image);
                    }

                }

                imageBox.Source = final;
                TransformImage = image;

                boxWidth = imageBox.RenderSize.Width;
                boxHeight = imageBox.RenderSize.Height;
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.ToString());
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.ShowDialog();

                System.Drawing.Bitmap final = BmpImage2Bmp(imageBox.Source as System.Windows.Media.Imaging.BitmapImage);
                final.Save(sfd.FileName);
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show(exc.ToString());
            }
        }




        private void Zoom(object sender, MouseWheelEventArgs e)
        {

            var st = (imageBox.RenderTransform as TransformGroup).Children[1] as ScaleTransform;

            double zoom = e.Delta > 0 ? .2 : -.2;
            st.ScaleX += zoom;
            st.ScaleY += zoom;
            boxWidth = imageBox.RenderSize.Width;
            boxHeight = imageBox.RenderSize.Height;

        }

        Point start;
        Point origin;

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imageBox.CaptureMouse();
            var tt = (TranslateTransform)((TransformGroup)imageBox.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
            start = e.GetPosition(ImageBorder);
            origin = new Point(tt.X, tt.Y);
            boxWidth = imageBox.RenderSize.Width;
            boxHeight = imageBox.RenderSize.Height;
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            boxWidth = imageBox.RenderSize.Width;
            boxHeight = imageBox.RenderSize.Height;
            if (imageBox.IsMouseCaptured)
            {
                var tt = (TranslateTransform)((TransformGroup)imageBox.RenderTransform)
                    .Children.First(tr => tr is TranslateTransform);
                Vector v = start - e.GetPosition(ImageBorder);
                tt.X = origin.X - v.X;
                tt.Y = origin.Y - v.Y;


            }

            else
            {
                start = Mouse.GetPosition(imageBox);
                int ScaledX = (int)((start.X / boxWidth) * (double)TransformImage.Width);
                int ScaledY = (int)((start.Y / boxHeight) * (double)TransformImage.Height);
                XTxt.Text = "X: " + ScaledX.ToString();
                YTxt.Text = "Y: " + ScaledY.ToString();
                ValueTxt.Text = "Value: " + TransformImage.GetPixel(ScaledX, ScaledY).R.ToString();

            }
        }
        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            imageBox.ReleaseMouseCapture();
        }
        

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            More.IsChecked = true;

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            imageBox.Source = null;
            TransformImage = null;
            Reserve = null;
            ValueTxt.Text = "";
            XTxt.Text = "";
            YTxt.Text = "";
            var st = (imageBox.RenderTransform as TransformGroup).Children[1] as ScaleTransform;
            st.ScaleX = 1;
            st.ScaleY = 1;
            var tt = (imageBox.RenderTransform as TransformGroup).Children[0] as TranslateTransform;
            tt.X = 0.5;
            tt.Y = 0.5;

        }

        private void openFileButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
