using System;

namespace CarSimulator
{
    public class Car : ICar
    {
        private readonly IEngine engine;

        private readonly IGps gps;

        //private readonly ISteeringWheelController steeringWheelController;

        public Car(IEngine engine, IGps gps)
        {
            this.engine = engine;
            this.gps = gps;
        }

        public bool AutoDrive(string destination)
        {
            if (!this.gps.CanDriveToDestination())
            {
                return false;
            }

            this.engine.Move();

            //var listOfMovements = this.gps.TraceRouteToDestination();
            //foreach (var movement in listOfMovements)
            //{
            //    if (movement == "right")
            //    {
            //        this.steeringWheelController.MoveRight();
            //    }
            //    else
            //    {
            //        this.steeringWheelController.MoveLeft();
            //    }
            //}

            return true;
        }
    }
}
