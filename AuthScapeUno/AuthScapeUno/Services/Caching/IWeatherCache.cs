namespace AuthScapeUno.Services.Caching;
using WeatherForecast = AuthScapeUno.Client.Models.WeatherForecast;
public interface IWeatherCache
{
    ValueTask<IImmutableList<WeatherForecast>> GetForecast(CancellationToken token);
}
