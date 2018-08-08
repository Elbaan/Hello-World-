using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows;

//
// Im using the Windows forms Drawing, because idk how to work with the WPF Drawing commands.
// TODO: Add Comments!

namespace WpfApplicationpaartest
{

    public partial class MainWindow : Window
    {

        int safebutton=0;

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref System.Drawing.Point lpPoint);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        SolidColorBrush mySolidColorBrush = new SolidColorBrush();

        public MainWindow()
        {
            InitializeComponent();

            this.Topmost = true;
            this.Focus();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int safe = Convert.ToInt32(slider.Value);
            textBox.Text = safe.ToString();

            mySolidColorBrush.Color = System.Windows.Media.Color.FromArgb(
                255,
                Convert.ToByte(textBox.Text),
                Convert.ToByte(textBox_Copy.Text),
                Convert.ToByte(textBox_Copy1.Text));
            ellipse.Fill = mySolidColorBrush;

            textBox1.Text = Convert.ToString(ellipse.Fill);
        }

        private void slider_Copy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int safe = Convert.ToInt32(slider_Copy.Value);
            textBox_Copy.Text = safe.ToString();

            mySolidColorBrush.Color = System.Windows.Media.Color.FromArgb(
                255,
                Convert.ToByte(textBox.Text),
                Convert.ToByte(textBox_Copy.Text),
                Convert.ToByte(textBox_Copy1.Text));
            ellipse.Fill = mySolidColorBrush;

            textBox1.Text = Convert.ToString(ellipse.Fill);
        }

        private void slider_Copy1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int safe = Convert.ToInt32(slider_Copy1.Value);
            textBox_Copy1.Text = safe.ToString();

            mySolidColorBrush.Color = System.Windows.Media.Color.FromArgb(
                255,
                Convert.ToByte(textBox.Text),
                Convert.ToByte(textBox_Copy.Text),
                Convert.ToByte(textBox_Copy1.Text));
            ellipse.Fill = mySolidColorBrush;

            textBox1.Text = Convert.ToString(ellipse.Fill);         
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            safebutton = 1;
            Cursor = Cursors.Pen;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (safebutton == 1)
            {
                safebutton = 0;
                Cursor = Cursors.Arrow;

                System.Windows.Point p = Mouse.GetPosition(this);

                System.Drawing.Point cursor = new System.Drawing.Point();
                GetCursorPos(ref cursor);

                label1.Content = System.Drawing.ColorTranslator.ToHtml(GetColorAt(cursor.X, cursor.Y));

                System.Windows.MessageBox.Show(Convert.ToString(label1.Content));

                string kreisel = Convert.ToString(label1.Content);

                kreisel = Convert.ToString(mySolidColorBrush);

                ellipse.Fill = mySolidColorBrush;         

            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);

        public static System.Drawing.Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);

            return System.Drawing.Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }
    }
}
