using System;

public interface WeatherService 
{
    double GetTemperatureCelsius(); 
}

public class Science_WeatherApi 
{
    private double kelvin; 

    public Science_WeatherApi(double kelvin) 
    {
        this.kelvin = kelvin;
    }

    public double GetKelvin() 
    {
        return kelvin;
    }
}

public class USA_WeatherApi 
{
    private double fahrenheit; 

    public USA_WeatherApi(double fahrenheit) 
    {
        this.fahrenheit = fahrenheit;
    }

    public double GetFahrenheit() 
    {
        return fahrenheit;
    }
}


public class ScienceWeatherAdapter : WeatherService 
{
    private Science_WeatherApi adaptee;

    public ScienceWeatherAdapter(Science_WeatherApi api) 
    {
        this.adaptee = api;
    }

    public double GetTemperatureCelsius() 
    {
        return adaptee.GetKelvin() - 273.15; 
    }

    public override string ToString()
    {
        return $"{adaptee.GetKelvin()} Kelvin (จาก Science API)";
    }
    
}

public class UsaWeatherAdapter : WeatherService 
{
    private USA_WeatherApi adaptee;

    public UsaWeatherAdapter(USA_WeatherApi api) 
    {
        this.adaptee = api;
    }

    public double GetTemperatureCelsius() 
    {
        return (adaptee.GetFahrenheit() - 32) * 5.0 / 9.0;
    }

    public override string ToString()
    {
        return $"{adaptee.GetFahrenheit()} Fahrenheit (จาก USA API)";
    }
}

public class Dashboard 
{
    private string dashboardName;
    private double alertTempCelsius; 

    public Dashboard(string name, double alertTemp) 
    {
        this.dashboardName = name;
        this.alertTempCelsius = alertTemp;
    }

    public double GetTemperatureCelsius() 
    {
        return alertTempCelsius; 
    }

    public bool CheckTempAlert(WeatherService w) 
    {
        
        return w.GetTemperatureCelsius() >= this.GetTemperatureCelsius();
    }
    public void DisplayWeather(WeatherService w)
    {
        double currentTemp = w.GetTemperatureCelsius();
        bool isAlert = CheckTempAlert(w);

        Console.WriteLine($"ข้อมูลต้นทาง: {w.ToString()}");
        Console.WriteLine($"[ระบบ: {dashboardName}] อุณหภูมิที่อ่านได้: {currentTemp} °C");
        
        if (isAlert)
        {
            Console.WriteLine($"แจ้งเตือน อุณหภูมิเกินกว่าเป้าหมายที่ตั้งไว้ ({alertTempCelsius} °C)");
        }
        else
        {
            Console.WriteLine("สถานะ: ปกติ (อุณหภูมิอยู่ในเกณฑ์)");
        }
        Console.WriteLine((new string('-', 50)));
    }
}

class Program 
{
    static void Main(string[] args) 
    {
        Dashboard myDashboard = new Dashboard("Server Room", 25.0);

        Science_WeatherApi scienceApi = new Science_WeatherApi(310.15); 
        USA_WeatherApi usaApi = new USA_WeatherApi(68.0);               

        ScienceWeatherAdapter adapterScience = new ScienceWeatherAdapter(scienceApi);
        UsaWeatherAdapter adapterUsa = new UsaWeatherAdapter(usaApi);

        Console.WriteLine($"Science API: {scienceApi.GetKelvin()} Kelvin");
        Console.WriteLine($"USA API: {usaApi.GetFahrenheit()} Fahrenheit");
        Console.WriteLine();
        
        myDashboard.DisplayWeather(adapterScience);
        myDashboard.DisplayWeather(adapterUsa);
        Console.ReadLine();
    }
}