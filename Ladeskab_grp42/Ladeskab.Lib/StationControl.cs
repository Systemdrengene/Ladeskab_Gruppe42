using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class StationControl : IObserver
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        public LadeskabState _state { get; set; }
        private IDoor _door;
        private IChargeControl _charger;
        private IDisplay _display;
        private FileLogger filelog;
        private IRFIDReader _rfidReader;
        private int _oldId;

        //private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        //constructor
        public StationControl(IChargeControl Charger, IDoor door, IRFIDReader rfidReader, IDisplay display, FileLogger logger)
        {
            _state = new LadeskabState();
            _charger = Charger;
            _door = door;
            _display = display;
            filelog = logger;
            _rfidReader = rfidReader;
        }

        public void Update(object subject, string msg)
        {
            switch(msg)
            {
                case "Door opened":
	                DoorEventHandler();
                    //_display.UpdateUserMsg("Tilslut telefon");
                    break;
                case "Door closed":
	                DoorEventHandler();
                    //_display.UpdateUserMsg("Indlæs RFID");
                    break;
                case "RFID":
                    _display.UpdateUserMsg("RFID has been read");
                    RfidDetected(_rfidReader.GetID());
                    break;

            }
        }


        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        filelog.LogFile( "Skab låst med RFID: " + id);
                        //using (var writer = File.AppendText(logFile))
                        //{
                        //    writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        //}
                        _display.UpdateUserMsg("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        // Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.UpdateUserMsg("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                        //Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    _display.UpdateUserMsg("Dør er allerede åbnet med et RF-ID");
                    //Console.WriteLine("Dør er allerede åbnet med et RF-ID");
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        filelog.LogFile("Skab låst op med RFID: " + id);
                        //using (var writer = File.AppendText(logFile))
                        //{
                        //    writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        //}

                        _display.UpdateUserMsg("Tag din telefon ud af skabet og luk døren");
                        //Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.UpdateUserMsg("Forkert RFID tag");
                        //Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

       // Her mangler de andre trigger handlere
        private void DoorEventHandler()  //eventuelt handler
        {

	        switch (_state)
	        {
                case LadeskabState.Available: 

	                if (_door.OnDoorOpen() == true)  //Kan åbne door - Ulåst
	                {
		                _state = LadeskabState.DoorOpen;
                        _display.UpdateUserMsg("Tilslut Telefon");
	                }
	                else
	                {
                        //throw execption?
	                }
	                break;

                case LadeskabState.DoorOpen:
	                if (_door.OnDoorClose() == false)  // Door er closed og ulåst
	                {
		                _state = LadeskabState.Available; 
                        _display.UpdateUserMsg("Indlæs RFID");
	                }
	                else
	                {
		                //throw Exception  Kan ikke åbne door
	                }
	                break;

                case LadeskabState.Locked:
                    //throw Exception  // Kan ikke åbne door hvis låst
	                break;
	        }
        }

 
    }
}
