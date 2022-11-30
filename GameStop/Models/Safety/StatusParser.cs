namespace GameStop.Models.Safety;

public class StatusParser
{
    public static String StatusPars(int statusId)
    {
        switch (statusId)
        {
            case 1: return "In selling"; 
            case 2: return "Sold out";
            case 3: return "Release soon...";
            default: return "N/A";
        }
    }
}