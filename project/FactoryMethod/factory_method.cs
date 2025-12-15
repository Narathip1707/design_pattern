using System;
namespace Task01.FactoryMethod
{
     // Product Interface
    public interface WorkoutProgram
    {
        void planWorkout();
        string getGoal();
        int calculateTime();
    }

    // Concrete Products
    public class CardioPlan : WorkoutProgram
    {
        public void planWorkout() {Console.WriteLine("CardioPlan WorkOut...");}
        public string getGoal() { return "Fat Burning!";}
        public int calculateTime() {return 60;}
    }

    public class StrengthPlan : WorkoutProgram
    {
        public void planWorkout() {Console.WriteLine("StrengthPlan WorkOut...");}
        public string getGoal() { return "build muscle!";}
        public int calculateTime() {return 60;}
    }

    public class YogaPlan : WorkoutProgram
    {
        public void planWorkout() {Console.WriteLine("YogaPlan WorkOut...");}
        public string getGoal() { return "Stretching!";}
        public int calculateTime() {return 40;}
    }

    // Abstract Creator
    public abstract class ProgramFactory
    {
        // FactoryMethod
        public abstract WorkoutProgram getWorkoutProgram();

        // Template Method
        public void CreateProgram()
        {
            WorkoutProgram program = getWorkoutProgram();

            Console.WriteLine("=== New Workout Plan Created ===");
            program.planWorkout();
            Console.WriteLine($"Goal: {program.getGoal()}");
            Console.WriteLine($"Total Time: {program.calculateTime()} minutes");
            Console.WriteLine("=======================");
        }
    }

    // Concrete Creator
    public class BeginnerPlanCreator : ProgramFactory
    {
        public override WorkoutProgram getWorkoutProgram()
        {
            return new CardioPlan();
        }
    }

    public class AdvancedPlanCreator : ProgramFactory
    {
        public override WorkoutProgram getWorkoutProgram()
        {
            return new StrengthPlan();
        }
    }

    public class YogaPlanCreator : ProgramFactory
    {
        public override WorkoutProgram getWorkoutProgram()
        {
            return new YogaPlan();
        }
    }

    class Starter
    {
        static void Main()
        {
            Console.WriteLine("===Function===");

            ProgramFactory factory;
            //Beginner
            factory = new BeginnerPlanCreator();
            factory.CreateProgram();
            //Advanced
            factory = new AdvancedPlanCreator();
            factory.CreateProgram();
            //Yoga
            factory = new YogaPlanCreator();
            factory.CreateProgram();


            Console.WriteLine("===Main===");
            ProgramFactory client = new BeginnerPlanCreator();
            WorkoutProgram program = client.getWorkoutProgram();
            Console.WriteLine($"Goal: {program.getGoal()}");
            Console.WriteLine($"Time: {program.calculateTime()} minutes");
            program.planWorkout();


            Console.WriteLine();

        }
    }
}


