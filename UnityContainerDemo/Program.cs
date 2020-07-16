using System;
using Unity;
using Unity.Injection;

namespace UnityContainerDemo
{

    class Program
    {
        static void Main(string[] args)
        {
            //Instance of Unity
            IUnityContainer container = new UnityContainer();
            //Unity instatiates a car
            container.RegisterType<ICar, BMW>();// Map ICar with BMW
            container.RegisterType<ICar, Audi>("LuxuryCar");

            //container.RegisterType<Driver>("LuxuryCarDriver", 
            //    new InjectionConstructor(container.Resolve<ICar>("Luxury Car")));

            //Driver driver1 = container.Resolve<Driver>();// injects BMW
            //driver1.RunCar();

            //Driver driver2 = container.Resolve<Driver>("LuxuryCarDriver");// injects Audi
            //driver2.RunCar();

            //ICar ford = new Ford();
            //container.RegisterInstance(ford);

            //Driver driver3 = container.Resolve<Driver>();
            //driver3.RunCar();


            // THIS WAS USED FOR CONSTRUCTOR INJECTION:
            //container.RegisterType<Driver>(new InjectionConstructor(new object[] { new Audi(), "Steve" }));
            //var driver = container.Resolve<Driver>(); // Injects Audi and Steve
            //driver.RunCar();

            //PROPERTY INJECTION
            var driver1 = container.Resolve<Driver>();
            driver1.RunCar();

            //run-time configuration
            //container.RegisterType<Driver>(new InjectionProperty("Car", new BMW()));

            var driver2 = container.Resolve<Driver>();
            driver2.RunCar();
        }
    }


    public interface ICar
    {
        int Run();
    }

    public class BMW : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Ford : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Audi : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }

    }
    public class Driver
    {
        // THIS WAS USED FOR CONSTRUCTOR INJECTION:
        //private ICar _car = null;
        //private string _name = string.Empty;

        //public Driver(ICar car, string driverName)
        //{
        //    _car = car;
        //    _name = driverName;
        //}

        //public void RunCar()
        //{
        //    Console.WriteLine("{0} is running {1} - {2} mile ",
        //                    _name, _car.GetType().Name, _car.Run());
        //}

        private ICar _car = null;

        public Driver()
        {
        }

        ////[Dependency]
        //[Dependency("LuxuryCar")] //Named mapping
        //public ICar Car { get; set; }

        [InjectionMethod]
        public void UseCar(ICar car)
        {
            _car = car;
        }

        public void RunCar()
        {
            Console.WriteLine("Running {0} - {1} mile ",
                                //this.Car.GetType().Name, this.Car.Run());
                                this._car.GetType().Name, this._car.Run());
        }
    }

}
