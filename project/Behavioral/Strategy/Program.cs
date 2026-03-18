using System;

namespace StrategyPhotoEditor
{
    
    public interface Strategy 
    {
        
        string Render(string img); 
    }

    
    public class Fade : Strategy 
    {
        public string Render(string img) 
        {
            return "Fade Filter applied to " + img;
        }
    }

    public class Paris : Strategy 
    {
        public string Render(string img) 
        {
            return "Paris Filter applied to " + img;
        }
    }

    public class Midnight : Strategy 
    {
        public string Render(string img) 
        {
            return "Midnight Filter applied to " + img;
        }
    }

    public class Gritty : Strategy 
    {
        public string Render(string img) 
        {
            return "Gritty Filter applied to " + img;
        }
    }

    public class Blue : Strategy 
    {
        public string Render(string img) 
        {
            return "Blue Filter applied to " + img;
        }
    }


    public class PhotoEditor 
    {

        private Strategy strategy; 

        public PhotoEditor(Strategy s = null) 
        {
            this.strategy = s;
        }

        public void SetFilter(Strategy s) 
        {
            this.strategy = s;
        }

        public void ApplyFilter(string img) 
        {
            if (this.strategy != null) 
            {
                Console.WriteLine("  " + this.strategy.Render(img));
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            PhotoEditor editor = new PhotoEditor();
            Console.WriteLine("PhotoEditor menu: ");

            editor.SetFilter(new Fade());
            editor.ApplyFilter("profile.jpg");

            editor.SetFilter(new Paris());
            editor.ApplyFilter("food.jpg");
            editor.ApplyFilter("cat.png");

            
            editor.SetFilter(new Midnight());
            editor.ApplyFilter("city_night.jpg");

            editor.SetFilter(new Gritty());
            editor.ApplyFilter("mountain.jpg");

            editor.SetFilter(new Blue());
            editor.ApplyFilter("ocean.jpg");
            Console.ReadLine();
        }
    }
}