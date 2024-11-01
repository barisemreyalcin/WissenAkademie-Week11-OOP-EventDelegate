namespace VehicleSpeedControlEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Vehicle vehicle = new Vehicle();
            vehicle.SpeedExceeded += new SpeedHandler(VehicleSpeedExceeded);

            for (int i = 70; i < 90; i++)
            {
                vehicle.Speed = i;
                Console.WriteLine($"Speed: {vehicle.Speed}");
                Thread.Sleep( 5000 );
            }
        }

        private static void VehicleSpeedExceeded(object sender, SpeedArgs args)
        {
            Console.WriteLine($"Speed limit exceeded: {args.CurSpeed} - {args.ExceedTime.ToLongDateString()}");
        }
    }

    public delegate void SpeedHandler(object sender, SpeedArgs args); 
    public class SpeedExceededEventArgs : EventArgs
    {
        public SpeedExceededEventArgs(int _curSpeed)
        {
            CurSpeed = _curSpeed;
        }
        private int curSpeed;

        public int CurSpeed { get => curSpeed; set => curSpeed = value; }
    }

    class Vehicle
    {
        public event SpeedHandler SpeedExceeded;
        private int speed;
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                if (speed > 80 && SpeedExceeded != null)
                    SpeedExceeded(this, new SpeedArgs(value));
            }
        }

        public VehicleModel Model { get; set; }
    }

    public class SpeedArgs
    {

        public SpeedArgs(int curSpeed)
        {
            this.CurSpeed = curSpeed;
        }
        private int curSpeed;

        public int CurSpeed { get => curSpeed; set => curSpeed = value; }

        private DateTime exceedTime = DateTime.Now;

        public DateTime ExceedTime
        {
            get { return exceedTime; }
        }
    }

    public enum VehicleModel : byte
    {
        Automobile,
        Minibus,
        Bus,
        Van,
        CommercialVehicle
    }
}
