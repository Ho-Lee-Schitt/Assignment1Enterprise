using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1CarFactory
{
    public enum CarType
    {
        BMW,
        KIA
    };
    public enum BMWType
    {
        ONESERIES = 1,
        TWOSERIES = 2,
        THREESERIES = 3,
        INVALID = 4
    };
    public enum KiaType
    {
        SPORTAGE = 1,
        RIO = 2,
        SORENTO = 3,
        INVALID = 4
    };
    abstract class Car
    {
        public abstract string Company { get; }
        public abstract string Model { get; set; }
        public abstract int Speed { get; set; }
    }// End Car
    class BMWCar : Car
    {
        public override string Company { get { return "BMW"; } }
        public override string Model { get; set; }
        public override int Speed { get; set; }
    }// End BMWCar
    class KiaCar : Car
    {
        public override string Company { get { return "Kia"; } }
        public override string Model { get; set; }
        public override int Speed { get; set; }
    }// End KiaCar
    abstract class CarFactory
    {
        public abstract Car GetCar();
    }// End CarFactory
    class BMWFactory : CarFactory
    {
        public override Car GetCar()
        {
            Car car;
            Console.WriteLine();
            Console.WriteLine("Select a car type:\n1) BMW 1 Series\n2) BMW 2 Series\n3) BMW 3 Series");
            Console.WriteLine();
            Console.Write("Enter your selection: ");
            int selection;
            if (int.TryParse(Console.ReadLine(), out selection))
            {
                BMWType carType;
                if (typeof(BMWType).IsEnumDefined(selection))
                    carType = (BMWType)selection;
                else
                    carType = BMWType.INVALID;
                switch (carType)
                {
                    case BMWType.ONESERIES:
                        car = new BMWCar();
                        car.Model = "1 Series";
                        car.Speed = 200;
                        break;
                    case BMWType.TWOSERIES:
                        car = new BMWCar();
                        car.Model = "2 Series";
                        car.Speed = 250;
                        break;
                    case BMWType.THREESERIES:
                        car = new BMWCar();
                        car.Model = "3 Series";
                        car.Speed = 300;
                        break;
                    case BMWType.INVALID:
                    default:
                        car = new BMWCar();
                        car.Model = "Invalid";
                        break;
                }
                return car;
            }
            else
            {
                car = new BMWCar();
                car.Model = "Invalid";
                return car;
            }
        }
    }// End BMWFactory
    class KiaFactory : CarFactory
    {
        public override Car GetCar()
        {
            Car car;
            Console.WriteLine();
            Console.WriteLine("Select a car type:\n1) Kia Sportage\n2) Kia Rio\n3) Kia Sorento");
            Console.WriteLine();
            Console.Write("Enter your selection: ");
            int selection;
            if (int.TryParse(Console.ReadLine(), out selection))
            {
                KiaType carType;
                if (typeof(KiaType).IsEnumDefined(selection))
                    carType = (KiaType)selection;
                else
                    carType = KiaType.INVALID;
                switch (carType)
                {
                    case KiaType.SPORTAGE:
                        car = new KiaCar();
                        car.Model = "Sportage";
                        car.Speed = 320;
                        break;
                    case KiaType.RIO:
                        car = new KiaCar();
                        car.Model = "Rio";
                        car.Speed = 180;
                        break;
                    case KiaType.SORENTO:
                        car = new KiaCar();
                        car.Model = "Sorento";
                        car.Speed = 100;
                        break;
                    case KiaType.INVALID:
                    default:
                        car = new KiaCar();
                        car.Model = "Invalid";
                        break;
                }
                return car;
            }
            else
            {
                car = new KiaCar();
                car.Model = "Invalid";
                return car;
            }
        }
    }// End KiaFactory
    public class CarAssembler
    {
        public int AssembleCar()
        {
            BMWFactory bmwFactory = new BMWFactory();
            KiaFactory kiaFactory = new KiaFactory();
            Car car;
            Console.WriteLine();
            Console.WriteLine("Select a car Brand:\n1) BMW\n2) Kia\n3) Exit");
            Console.WriteLine();
            Console.Write("Enter your selection: ");
            int selection;
            if (int.TryParse(Console.ReadLine(), out selection))
            {
                switch(selection)
                {
                    case 1:
                        car = bmwFactory.GetCar();
                        break;
                    case 2:
                        car = kiaFactory.GetCar();
                        break;
                    case 3:
                        Console.WriteLine("Goodbye");
                        return 0;
                    default:
                        Console.WriteLine("Your selection was invalid. Please try again.");
                        return 1;
                }
                if (String.Compare(car.Model, "Invalid") == 0)
                {
                    Console.WriteLine("Your selection was invalid. Please try again.");
                    return 1;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Your Car details are:");
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine(String.Format("|{0,-30}|{1,-10}|", "Car Make & Model", "Top Speed"));
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine(String.Format("|{0,-30}|{1,-10}|", (car.Company + " " + car.Model), car.Speed));
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine();
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("Your selection was invalid. Please try again.");
                return 1;
            }
        }
    }// End CarAssembler
    class Program
    {
        static void Main(string[] args)
        {
            CarAssembler assembler = new CarAssembler();

            Console.WriteLine("*****************");
            Console.WriteLine("Car Factory Demo");
            Console.WriteLine("*****************");

            while (assembler.AssembleCar() != 0) {}

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }// End Main
    }// End Program
}
