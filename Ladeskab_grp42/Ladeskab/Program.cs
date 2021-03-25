using System;
using Ladeskab.Lib;
using UsbSimulator;

namespace Ladeskab 
{ 
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            Door door = new();
            RFIDReader rfidReader = new();
            Display display = new();
            UsbChargerSimulator usbCharger = new();
            FileLogger fileLogger = new(new FileWriter, new FileReader);
            ChargeControl chargeControl = new(display, usbCharger);
            StationControl stationControl = new(chargeControl, door, rfidReader, display, fileLogger);
            
            door.Attach(stationControl);
            rfidReader.Attach(stationControl);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }
            } while (!finish);
        }
    }
}
