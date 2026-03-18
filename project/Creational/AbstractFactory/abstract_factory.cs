using System;
namespace Aquarium 
{

    // Product
    public interface Fish
    {
        void Setup();
    }
    public interface Plant
    {
        void Setup();
    }
    public interface Substrate
    {
        void Setup();
    }
    // ConcreteProducts
    //--------------
    // Freshwater(น้ำจืด)
    public class FreshwaterFish : Fish
    {
        public void Setup(){Console.WriteLine("ใส่ปลาน้ำจืดแล้ว");}
    }
    public class FreshwaterPlant : Plant
    {
        public void Setup(){Console.WriteLine("ใส่พืชน้ำจืดแล้ว");}
    }
    public class FreshwaterSubstrate : Substrate
    {
        public void Setup(){Console.WriteLine("ใส่รองพื้นตู้ปลาสำหรับปลาน้ำจืดแล้ว");}
    }
    // Saltwater(น้ำเค็ม)
    public class SaltwaterFish : Fish
    {
        public void Setup(){Console.WriteLine("ใส่ปลาน้ำเค็มแล้ว");}
    }
    public class SaltwaterPlant : Plant
    {
        public void Setup(){Console.WriteLine("ใส่พืชน้ำเค็มแล้ว");}
    }
    public class SaltwaterSubstrate : Substrate
    {
        public void Setup(){Console.WriteLine("ใส่รองพื้นตู้ปลาสำหรับปลาน้ำเค็มแล้ว");}
    }
    // Brackish(น้ำกร่อย)
    public class BrackishFish : Fish
    {
        public void Setup(){Console.WriteLine("ใส่ปลาน้ำกร่อยแล้ว");}
    }
    public class BrackishPlant : Plant
    {
        public void Setup(){Console.WriteLine("ใส่พืชน้ำกร่อยแล้ว");}
    }
    public class BrackishSubstrate : Substrate
    {
        public void Setup(){Console.WriteLine("ใส่รองพื้นตู้ปลาสำหรับปลาน้ำกร่อยแล้ว");}
    }
    // AbstactFactory
    public interface AquariumFactory
    {
        Fish CreateFish();
        Plant CreatePlant();
        Substrate CreateSubstrate();
    }
    // ConcreteFactory
    public class FreshwaterFactory : AquariumFactory
    {
        public Fish CreateFish()
        {
            return new FreshwaterFish();
        }
        public Plant CreatePlant()
        {
            return new FreshwaterPlant();
        }
        public Substrate CreateSubstrate()
        {
            return new FreshwaterSubstrate();
        }
    }
    public class SaltwaterFactory : AquariumFactory
    {
        public Fish CreateFish()
            {
                return new SaltwaterFish();
            }
        public Plant CreatePlant()
            {
                return new SaltwaterPlant();
            }
        public Substrate CreateSubstrate()
            {
                return new SaltwaterSubstrate();
            }
    }
    public class BrackishFactory : AquariumFactory
    {
        public Fish CreateFish()
        {
            return new BrackishFish();
        }
        public Plant CreatePlant()
        {
            return new BrackishPlant();
        }
        public Substrate CreateSubstrate()
        {
            return new BrackishSubstrate();
        }
    }
    // Client Class
    public class Client
    {
        private AquariumFactory factory;
        // private Fish fish;
        // private Plant plant;
        // private Substrate substrate;

        private Fish fish = null!;
        private Plant plant = null!;
        private Substrate substrate = null!;

        public Client(AquariumFactory f)
        {
            factory = f;
            CreateAquarium();
        }
        public void CreateAquarium()
        {
            fish = factory.CreateFish();
            plant = factory.CreatePlant();
            substrate = factory.CreateSubstrate();
        }
        public void Setup()
        {
            fish.Setup();
            plant.Setup();
            substrate.Setup();
        }
    }

    class Program
    {
        static void Main()
        {
            AquariumFactory factory;
            Client client;
            //น้ำจืด
            factory = new FreshwaterFactory();
            client = new Client(factory);
            client.Setup();
            Console.WriteLine("======");
            //น้ำเค็ม
            factory = new SaltwaterFactory();
            client = new Client(factory);
            client.Setup();
            Console.WriteLine("======");
            //น้ำกร่อย
            factory = new BrackishFactory();
            client = new Client(factory);
            client.Setup();
            Console.WriteLine("======");

            
            factory = new FreshwaterFactory();
            Fish fish = factory.CreateFish();
            Plant plant = factory.CreatePlant();
            Substrate substrate = factory.CreateSubstrate();
            fish.Setup();
            plant.Setup();
            substrate.Setup();

            Console.ReadLine();

        }
    }
}