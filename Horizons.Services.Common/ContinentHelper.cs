using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Services.Common;

public static class ContinentHelper
{
    public static List<string> GetContinents()
    {
        return new List<string>
        {
            "Africa",
            "Antarctica",
            "Asia",
            "Europe",
            "North America",
            "South America",
            "Oceania"
        };
    }

    public static string GetContinentForCoordinates(double lat, double lng)
    {
        
        if (lat > 35 && lng < -20) return "North America";
        if (lat < -10 && lng > 100) return "Oceania";
        if (lat > 35 && lng > 20 && lng < 60) return "Asia";
        if (lat > 35 && lng > -20 && lng < 30) return "Europe";
        if (lat < 0 && lat > -35 && lng > -50) return "South America";
        if (lat > 0 && lat < 35 && lng > -20 && lng < 50) return "Africa";

        return "Other";
    }
}