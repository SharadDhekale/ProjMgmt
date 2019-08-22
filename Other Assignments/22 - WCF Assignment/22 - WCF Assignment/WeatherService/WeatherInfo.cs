namespace WeatherService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class WeatherInfo : IWeatherInfo
    {
        public double celciustofarenheit(double temp)
        {
            double converter = temp * 1.8 + 32;
            return converter;
        }

        public double farenheittocelcius(double temp)
        {
            double converter = (temp - 32) / 1.8;
            return converter;
        }
    }
}
