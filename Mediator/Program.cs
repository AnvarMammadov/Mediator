using System.Numerics;

namespace Mediator
{

    public interface IAirTrafficControl
    {
        void RegisterAircraft(Aircraft aircraft);
        void SendWarning(Aircraft aircraft,string warning);   
    }


    public abstract class Aircraft
    {
        protected IAirTrafficControl  airTrafficControl;

        public Aircraft(IAirTrafficControl mediator) { 
            this.airTrafficControl = mediator;
        }

        public abstract void Send(string warning);
        public abstract void Receive(string warning);
    }

    public class Boeing747 : Aircraft
    {

        public Boeing747(IAirTrafficControl mediator) : base(mediator) { }
        public override void Send(string warning)
        {
            Console.WriteLine($"Boeing747 sending message : {warning}");
            airTrafficControl.SendWarning(this,warning);
        }

        public override void Receive(string warning)
        {
            Console.WriteLine($"Boeing747 received message : {warning}");
        }
    }


    public class AirbusA380 : Aircraft
    {

        public AirbusA380(IAirTrafficControl mediator) : base(mediator) { }
        public override void Send(string warning)
        {
            Console.WriteLine($"AirbusA380 sending message : {warning}");
            airTrafficControl.SendWarning(this, warning);
        }

        public override void Receive(string warning)
        {
            Console.WriteLine($"AirbusA380 received message : {warning}");
        }
    }


    public class AirTrafficControl : IAirTrafficControl
    {
        private List<Aircraft> aircrafts = new List<Aircraft>();

        public void RegisterAircraft(Aircraft aircraft)
        {
            if(!aircrafts.Contains(aircraft))
            {
                aircrafts.Add(aircraft);
            }
        }

        public void SendWarning(Aircraft aircraft, string warning)
        {
            foreach (var plane in aircrafts)
            {
                if (plane != aircraft)
                {
                    plane.Receive(warning);
                }
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            IAirTrafficControl airTrafficControl = new AirTrafficControl();

            Aircraft boeing747 = new Boeing747(airTrafficControl);
            Aircraft airbusA380 = new AirbusA380(airTrafficControl);

            airTrafficControl.RegisterAircraft(boeing747);
            airTrafficControl.RegisterAircraft(airbusA380);

            boeing747.Send("Warning! Decrease altitude.");
            airbusA380.Send("Roger that. Adjusting altitude.");

            Console.ReadKey();
        }
    }
}