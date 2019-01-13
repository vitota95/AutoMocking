namespace CarSimulator
{
    public interface IGps
    {
        string[] TraceRouteToDestination();
        bool CanDriveToDestination();
    }
}