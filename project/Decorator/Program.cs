using System;

namespace DecoratorStreaming
{
    //Interface
    public interface ISubscription
    {
        string GetDescription();
        double CalculatePrice();
        string GetOwner();
    }

    //Concrete Component
    public class BasicPlan : ISubscription
    {
        private double price = 150.0;
        private string resolution = "480p"; 

        private string ownerName;  

        public BasicPlan(string name)
        {
            this.ownerName = name; 
        }

        public string GetOwner()
        {
            return ownerName;
        }
        public string GetDescription()
        {
            
            return $"[{ownerName}] Basic Plan (SD) " + resolution;
        }

        public double CalculatePrice()
        {
            return price; 
        }
    }

    //Base Decorator
    public abstract class SubscriptionDecorator : ISubscription
    {
        private ISubscription subscription;

        public SubscriptionDecorator(ISubscription s)
        {
            this.subscription = s;
        }

        public virtual string GetDescription()
        {
            return subscription.GetDescription();
        }

        public virtual double CalculatePrice()
        {
            return subscription.CalculatePrice();
        }

        public virtual string GetOwner()
        {
            return subscription.GetOwner();
        }
    }

    //Concrete Decorators

    public class NoAdsDecorator : SubscriptionDecorator
    {
        public NoAdsDecorator(ISubscription s) : base(s) { }

        public override string GetDescription()
        {
            return base.GetDescription() + " + No Ads";
        }

        public override double CalculatePrice()
        {
            return base.CalculatePrice() + 50.0; 
        }
    }

    public class HD_Decorator : SubscriptionDecorator
    {
        public HD_Decorator(ISubscription s) : base(s) { }

        public override string GetDescription()
        {
            return base.GetDescription() + " + With HD";
        }

        public override double CalculatePrice()
        {
            return base.CalculatePrice() + 80.0;
        }
    }

    public class FullHD_Decorator : SubscriptionDecorator
    {
        public FullHD_Decorator(ISubscription s) : base(s) { }

        public override string GetDescription()
        {
            return base.GetDescription() + " + With Full HD";
        }

        public override double CalculatePrice()
        {
            return base.CalculatePrice() + 120.0; 
        }
    }

    public class UltraHD_Decorator : SubscriptionDecorator
    {
        public UltraHD_Decorator(ISubscription s) : base(s) { }

        public override string GetDescription()
        {
            return base.GetDescription() + " + With 4K Ultra HD";
        }

        public override double CalculatePrice()
        {
            return base.CalculatePrice() + 200.0; 
        }
    }

    public class Program
    {   

        static void Display(ISubscription sub)
        {   
            Console.WriteLine("\n----------------------------------");
            Console.WriteLine(sub.GetDescription());
            Console.WriteLine($"Price: {sub.CalculatePrice():N2} THB\n");
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Nara Streaming  ===\n");

            ISubscription myPlan = new BasicPlan("Narathip");
            Display(myPlan); 
            
            ISubscription planWithHD = new HD_Decorator(myPlan);
            Display(planWithHD);
            ISubscription hdNoAdsPlan = new NoAdsDecorator(planWithHD);
            Display(hdNoAdsPlan);

            ISubscription yourPlan = new BasicPlan("Somsak");
            Display(yourPlan);
            ISubscription premiumPlan = new UltraHD_Decorator(new NoAdsDecorator(yourPlan));
            Display(premiumPlan);

            ISubscription myplanALL = new NoAdsDecorator(new FullHD_Decorator(new BasicPlan("Benyapha")));
            Display(myplanALL);

            Console.WriteLine("\n----------------------------------");

            ISubscription seenPlan = new BasicPlan("Seen");
            seenPlan = new FullHD_Decorator(seenPlan);
            seenPlan = new NoAdsDecorator(seenPlan);
            Display(seenPlan);

            
            Console.ReadLine();
        }
    }
}