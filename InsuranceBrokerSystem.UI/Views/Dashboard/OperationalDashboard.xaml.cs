using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;
using SkiaSharp;
using LiveChartsCore.Defaults;
using System.Windows.Threading;

namespace InsuranceBrokerSystem.UI.Views.Dashboard
{
    public partial class OperationalDashboard : UserControl
    {
        public DashboardViewModel ViewModel { get; set; }

        public OperationalDashboard()
        {
            InitializeComponent();
            ViewModel = new DashboardViewModel();
            DataContext = ViewModel;
            
            // Cleanup on unload to prevent timer memory leaks
            this.Unloaded += (s, e) => ViewModel.StopLiveUpdates();
        }
    }

    public class DashboardViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer _liveTimer;
        private Random _rnd = new Random();

        // ---------------- METRICS ---------------- 
        private int _totalPolicies = 1245;
        public int TotalPolicies 
        { get => _totalPolicies; set { _totalPolicies = value; OnPropertyChanged(); } }

        private double _totalPremium = 4.2;
        public double TotalPremium 
        { get => _totalPremium; set { _totalPremium = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPremiumStr)); } }
        public string TotalPremiumStr => $"${TotalPremium:F2}M";

        private int _activeClaims = 84;
        public int ActiveClaims 
        { get => _activeClaims; set { _activeClaims = value; OnPropertyChanged(); } }

        private int _newClients = 128;
        public int NewClients 
        { get => _newClients; set { _newClients = value; OnPropertyChanged(); } }

        // ---------------- CHARTS ----------------
        public ObservableCollection<ISeries> TrendSeries { get; set; }
        private ObservableCollection<ObservableValue> _trendData;

        public IEnumerable<ISeries> PieSeries { get; set; }

        // Axes for the trend chart
        public Axis[] XAxes { get; set; } = { new Axis { IsVisible = false } };
        public Axis[] YAxes { get; set; } = { new Axis { IsVisible = false } };

        public ObservableCollection<Activity> RecentActivities { get; set; }

        public DashboardViewModel()
        {
            LoadBaseData();
            InitializeCharts();
            StartLiveUpdates();
        }

        private void LoadBaseData()
        {
            RecentActivities = new ObservableCollection<Activity>
            {
                new Activity { Action = "New Motor Policy bound for ABC Corp", User = "Admin", Time = "10 mins ago", Status = "Completed" },
                new Activity { Action = "Claim #CL-20260401 submitted", User = "J. Smith", Time = "45 mins ago", Status = "Pending" },
                new Activity { Action = "Endorsement approved for Policy #POL-992", User = "Admin", Time = "2 hours ago", Status = "Completed" },
                new Activity { Action = "Quotation sent to XYZ Enterprise", User = "M. Johnson", Time = "3 hours ago", Status = "In Progress" },
                new Activity { Action = "Payment received for Invoice #INV-221", User = "Finance", Time = "Yesterday", Status = "Completed" }
            };
        }

        private void InitializeCharts()
        {
            // TREND CHART (Line Chart)
            _trendData = new ObservableCollection<ObservableValue>
            {
                new ObservableValue(2), new ObservableValue(4), new ObservableValue(3),
                new ObservableValue(7), new ObservableValue(6), new ObservableValue(9),
                new ObservableValue(10)
            };

            TrendSeries = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservableValue>
                {
                    Values = _trendData,
                    Fill = new SolidColorPaint(new SKColor(26, 35, 126, 50)), // transparent PrimaryColor
                    Stroke = new SolidColorPaint(new SKColor(26, 35, 126)), // PrimaryColor
                    GeometrySize = 10,
                    LineSmoothness = 0.5
                }
            };

            // PIE CHART
            PieSeries = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 45 }, Name = "Motor", InnerRadius = 40, Pushout = 5 },
                new PieSeries<double> { Values = new double[] { 25 }, Name = "Health", InnerRadius = 40 },
                new PieSeries<double> { Values = new double[] { 15 }, Name = "Property", InnerRadius = 40 },
                new PieSeries<double> { Values = new double[] { 15 }, Name = "Marine", InnerRadius = 40 }
            };
        }

        private void StartLiveUpdates()
        {
            _liveTimer = new DispatcherTimer();
            _liveTimer.Interval = TimeSpan.FromSeconds(2.5);
            _liveTimer.Tick += (s, e) =>
            {
                // Slightly randomize metrics
                if (_rnd.Next(10) > 4) TotalPolicies += 1;
                if (_rnd.Next(10) > 6) TotalPremium += 0.01;
                if (_rnd.Next(10) > 7) ActiveClaims += 1;
                if (_rnd.Next(10) > 5) NewClients += 1;

                // Push new data to the right side of the chart, remove from the left
                _trendData.RemoveAt(0);
                var lastVal = _trendData[_trendData.Count - 1].Value ?? 10;
                var nextVal = lastVal + (_rnd.NextDouble() * 4 - 1.5); // Random walk
                _trendData.Add(new ObservableValue(nextVal > 0 ? nextVal : 1));
            };
            _liveTimer.Start();
        }

        public void StopLiveUpdates()
        {
            _liveTimer?.Stop();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Activity
    {
        public string Action { get; set; }
        public string User { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
    }
}
