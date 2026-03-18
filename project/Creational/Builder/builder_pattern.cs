using System;
namespace TravelBuilder
{
    
    enum TripType
    {
        Luxury,
        Backpacker
    }
    
    class Trip
    {

    //Product
        private TripType tripType;
        private string tripName;
        private bool hasInsurance = false;

        public Trip(TripType type)
        {
            this.tripType = type;
        }
        
        public Trip(TripType type, string name)
        {
            this.tripType = type;
            this.tripName = name;
        }
        public void SetFlight()
        {
            Console.WriteLine($"จองตั๋วเครื่องบินสำหรับทริป {this.tripType} แล้ว...");
        }
        public void SetHotel()
        {
            Console.WriteLine($"จองที่พักสำหรับทริป {this.tripType} แล้ว...");
        }
        public void SetActivity()
        {
            Console.WriteLine($"วางแผนกิจกรรมการท่องเที่ยวของทริป {this.tripType} แล้ว...");
        }
        public void SetInsurance()
        {
            this.hasInsurance = true;
            Console.WriteLine($"ซื้อประกันสำหรับทริป {this.tripType} แล้ว...");
        }
        public void SetName(string name)
        {
            this.tripName = name;
            Console.WriteLine($"ทริปชื่อ {this.tripName}");
        }
        public void Show()
        {
            Console.WriteLine("\n===== สรุปข้อมูลทริป =====");
            Console.WriteLine($"ประเภททริป : {tripType}");
            Console.WriteLine($"สถานะประกัน : {(hasInsurance ? "มีประกันการเดินทาง" : "ไม่มีประกันการเดินทาง")}");
        }
    }

    //Builder Interface
    interface Builder
    {
        void Reset();
        void BuildFlight();
        void BuildHotel();
        void BuildActivity();
        void BuildInsurance();
        void BuildName(string name);
    }

    //Concrate Builder
    class LuxuryTripBuilder : Builder
    {
        private Trip trip;
        public LuxuryTripBuilder()
        {
            this.trip = new Trip(TripType.Luxury);
        }
        public void Reset()
        {
            this.trip = new Trip(TripType.Luxury);
        }
        public void BuildName(string name)
        {
            this.trip.SetName(name);
        }
        public void BuildFlight()
        {
            this.trip.SetFlight();
        }
        public void BuildHotel()
        {
            this.trip.SetHotel();
        }
        public void BuildActivity()
        {
            this.trip.SetActivity();
        }
        public void BuildInsurance()
        {
            this.trip.SetInsurance();
        }
        public Trip GetResult()
        {
            return this.trip;
        }
    }

    class BackPackerTripBuilder : Builder
    {
        private Trip trip;
        public BackPackerTripBuilder()
        {
            this.trip = new Trip(TripType.Backpacker);
        }
        public void Reset()
        {
            this.trip = new Trip(TripType.Backpacker);
        }
        public void BuildFlight()
        {
            this.trip.SetFlight();
        }
        public void BuildHotel()
        {
            this.trip.SetHotel();
        }
        public void BuildActivity()
        {
            this.trip.SetActivity();
        }
        public void BuildInsurance()
        {
            this.trip.SetInsurance();
        }
         public void BuildName(string name)
        {
            this.trip.SetName(name);
        }
        public Trip GetResult()
        {
            return this.trip;
        }
    }

    class Director
    {
        private Builder builder;
        public Director(Builder builder)
        {
            this.builder = builder;
        }
        public void ChangeBuilder(Builder builder)
        {
            this.builder = builder;
            Console.WriteLine("เปลี่ยนรูปแบบ Builder แล้ว...");
        }

        public void BuildFlightOnlyTrip(string name)
        {
            builder.Reset();
            builder.BuildName(name);
            builder.BuildFlight();
        }


        public void BuildQuickTrip(string name)
        {
            this.builder.Reset();
            builder.BuildName(name); // ทำไมใช้ this.builder ไม่ได้ เพราะ this.builder เป็นตัวแปรของคลาส Director และไม่สามารถใช้ this ในการเรียกเมธอดของ Builder ได้โดยตรง ต้องใช้ builder.BuildName(name) แทน
            this.builder.BuildFlight();
            this.builder.BuildHotel();
        }
        public void BuilderFullPackageTrip(string name)
        {
            this.builder.Reset();
            builder.BuildName(name); 
            this.builder.BuildFlight();
            this.builder.BuildHotel();
            this.builder.BuildActivity();
            this.builder.BuildInsurance();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Luxury
            LuxuryTripBuilder luxuryBuilder = new LuxuryTripBuilder();
            Director director = new Director(luxuryBuilder);
            Console.WriteLine("---FlightOnly---");
            director.BuildFlightOnlyTrip("ทริป 1 ");
            luxuryBuilder.GetResult().Show();
            Console.WriteLine("---Quick---");
            director.BuildQuickTrip("ทริป 2 ");
            luxuryBuilder.GetResult().Show();
            Console.WriteLine("---Full---");
            director.BuilderFullPackageTrip("ทริป 3");
            luxuryBuilder.GetResult().Show();

            //BackPacker
            BackPackerTripBuilder backPackerBuilder = new BackPackerTripBuilder();
            director.ChangeBuilder(backPackerBuilder);
            Console.WriteLine("------");
            director.BuilderFullPackageTrip("ทริป 4");
            backPackerBuilder.GetResult().Show();

            LuxuryTripBuilder luxuryBuilder_Main = new LuxuryTripBuilder();
            luxuryBuilder_Main.BuildName("ทริปครอบครัว");
            luxuryBuilder_Main.Reset();
            luxuryBuilder_Main.BuildFlight();
            luxuryBuilder_Main.BuildHotel();
            luxuryBuilder_Main.BuildActivity();
            luxuryBuilder_Main.BuildInsurance();
            Trip luxuryTrip = luxuryBuilder_Main.GetResult();
            luxuryTrip.Show();



        }
    }
}