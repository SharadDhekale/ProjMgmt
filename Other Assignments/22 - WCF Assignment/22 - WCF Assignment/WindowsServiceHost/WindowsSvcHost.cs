using System.ServiceModel;
using System.ServiceProcess;

namespace WindowsServiceHost
{
    public partial class WindowsSvcHost : ServiceBase
    {
        ServiceHost host;
        public WindowsSvcHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            host = new ServiceHost(typeof(WeatherService.WeatherInfo));
            host.Open();
        }

        protected override void OnStop()
        {
            if (host != null)
                host.Close();
        }
    }
}
