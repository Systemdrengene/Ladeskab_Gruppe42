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
            FileLogger fileLogger = new(new FileWriter(), new FileReader(), "./log.txt");
            ChargeControl chargeControl = new(display, usbCharger);
            StationControl stationControl = new(chargeControl, door, rfidReader, display, fileLogger);

            //door.Attach(stationControl);
            //rfidReader.Attach(stationControl);
            System.Console.WriteLine("Indtast E, O, C, R: ");
            bool finish = false;
            do
            {
                string input = null;

                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                Console.Clear();

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
                        Console.Clear();
                        int id = Convert.ToInt32(idString);
                        //rfidReader.OnRfidRead(id);

                        rfidReader.ScanRFfidTag(id);
                        break;

                    default:
                        break;
                }

                System.Console.WriteLine("Indtast E, O, C, R");

            } while (!finish);
        }
    }
}
