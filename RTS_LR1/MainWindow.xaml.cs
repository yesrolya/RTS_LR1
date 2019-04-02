using System;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace RTS_LR1
{
    public partial class MainWindow : Window
    {
        System.DateTime currentTime;
        System.TimeSpan ts;
        DateTime date_diplom;
        TimeSpan start_programm;
        private System.Timers.Timer aTimer;
        private System.Windows.Threading.DispatcherTimer bTimer;
        private System.Windows.Forms.Timer cTimer;
        //private System.Diagnostics.Stopwatch sw;
        bool visibleImg0 = true;
        int h, w;
        void Initialize()
        {
            Img0.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/1.png"));
            Img2.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/3.png"));
            Img2.Stretch = Img0.Stretch = System.Windows.Media.Stretch.Uniform;
            Img2.Visibility = Img0.Visibility = Visibility.Visible;
            Img2.Height = Img0.Height = 200;
            h = (int)Img2.Height;
            w = (int)Img2.Width;
            NowDateTime.Text = DateTime.Now.ToString("hh:mm:ss MM/dd/yyyy ") + DateTime.Now.DayOfWeek.ToString() ; 
        }

        void StartMyTimer ()
        {
            start_programm = new TimeSpan(0);
            date_diplom = new DateTime(2019, 7, 7); //дата защиты диплома
            long nowTicks = DateTime.Now.Ticks;
            currentTime = new DateTime(nowTicks - nowTicks % 10000000);

            Set_aTimer();
            Set_bTimer();
            Set_cTimer();
            Set_dTimer();
        }
        
        private void Set_aTimer()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            //таймер этого типа не может работать с GUI выводится в консоли
            aTimer = new System.Timers.Timer()
            {
                Interval = 1000,
                AutoReset = true,
                Enabled = true
            };
            aTimer.Elapsed += new ElapsedEventHandler(
            delegate (Object source, System.Timers.ElapsedEventArgs e) 
            {
                sw.Stop();
                Console.WriteLine(@"{0,-45}: {1, 12}", "System.Timers.Timer: ", sw.Elapsed.ToString());
                currentTime = currentTime.AddSeconds(1);
                TimeSpan temp = (date_diplom - currentTime);
                Console.WriteLine(temp);
            });
        }

        private void Set_bTimer()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            //таймер WPF
            ts = new TimeSpan(0, 0, 1);
            bTimer = new System.Windows.Threading.DispatcherTimer();
            bTimer.Tick += (sender, e) =>
            {
                sw.Stop();
                Console.WriteLine(@"{0,-45}: {1, 12}", "System.Windows.Threading.DispatcherTimer: ", sw.Elapsed.ToString());

                if (visibleImg0)
                    Img0.Visibility = Visibility.Hidden;
                else
                    Img0.Visibility = Visibility.Visible;
                visibleImg0 = !visibleImg0;
                start_programm += ts;
                StartTime.Text = start_programm.ToString();
            };
            bTimer.Interval = ts;
            bTimer.Start();
        }
        
        private void Set_cTimer()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            //таймер WindowsForms
            cTimer = new System.Windows.Forms.Timer();
            cTimer.Tick += (sender, e) =>
            {
                sw.Stop();
                Console.WriteLine(@"{0,-45}: {1, 12}", "System.Windows.Forms.Timer:", sw.Elapsed.ToString());
                TimeSpan temp = (date_diplom - currentTime);
                DiplomTime.Text = temp.ToString();
            };
            cTimer.Interval = 1000;
            cTimer.Start();
        }

        private void Set_dTimer()
        {
            DoubleAnimation img2AnimateHeight = new DoubleAnimation
            {
                From = h,
                To = h - 20,
                Duration = TimeSpan.FromSeconds(1),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(20)
            };
            DoubleAnimation img2AnimateWidth = new DoubleAnimation
            {
                From = w,
                To = w - 20,
                Duration = TimeSpan.FromSeconds(1),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(20)
            };
            Img2.BeginAnimation(Image.HeightProperty, img2AnimateHeight);
            Img2.BeginAnimation(Image.WidthProperty, img2AnimateWidth);
            
        }

        public struct SystemTime
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Millisecond;
        };
        [DllImport("kernel32.dll", EntryPoint = "GetSystemTime", SetLastError = true)]
        public extern static void Win32GetSystemTime(ref SystemTime sysTime);

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SystemTime sysTime);

        void SetNewDate (DateTime date)
        {
            SystemTime updatedTime = new SystemTime();
            updatedTime.Year = (ushort)date.Year;
            updatedTime.Month = (ushort)date.Month;
            updatedTime.Day = (ushort)date.Day;
            updatedTime.Hour = (ushort)0;
            updatedTime.Minute = (ushort)0;
            updatedTime.Second = (ushort)0;

            Win32SetSystemTime(ref updatedTime);
        }

        public MainWindow()
        {
            InitializeComponent();

            //SetNewDate(new DateTime(2009,2,2));

            Initialize();
            StartMyTimer();

        }
    }
}
