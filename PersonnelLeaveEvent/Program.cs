
namespace PersonnelLeaveEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Personnel personnel = new Personnel()
            {
                Name = "John",
                Surname = "Doe",
                RegistrationNumber = "12345678910",
                IdentityNumber = 10987654321,
                LeaveDaysNumber = 20
            };
            personnel.LeaveControlEvent += new Personnel.PersonnelLeaveControlEventHandler(RemainingLeaveDayAlert);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"How many leave days will {personnel.Name} {personnel.Surname} use?");
                byte enteredLeaveDays = byte.Parse( Console.ReadLine());
                personnel.LeaveDaysNumber -= enteredLeaveDays;
                if (personnel.LeaveDaysNumber < 5)
                    break;

                Console.WriteLine($"{personnel.Name} {personnel.Surname}'s remainin leave days count: {personnel.LeaveDaysNumber}");

                Console.WriteLine(new string('-', 100));
            }
        }

        private static void RemainingLeaveDayAlert()
        {
            Console.WriteLine("Remaining leave days reached to 5.");
        }
    }
    class Personnel
    {
        public delegate void PersonnelLeaveControlEventHandler();
        public event PersonnelLeaveControlEventHandler LeaveControlEvent;

        public long IdentityNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        private int leaveDaysNumber;
        public int LeaveDaysNumber
        {
            get => leaveDaysNumber;
            set
            {
                leaveDaysNumber = value;
                if (leaveDaysNumber < 5 && LeaveControlEvent != null)
                    LeaveControlEvent();
            }
        }
    }

}
