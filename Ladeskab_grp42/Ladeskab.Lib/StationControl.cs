using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Lib.Interfaces;

namespace Ladeskab.Lib
{
    public class StationControl
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
        private IFileLogger filelog;
        private IRFIDReader _rfidReader;
        private int _oldId;


        //constructor
        public StationControl(IChargeControl Charger, IDoor door, IRFIDReader rfidReader, IDisplay display, IFileLogger logger)
        {
            _state = new LadeskabState();
            _charger = Charger;
            _door = door;
            _display = display;
            filelog = logger;
            _rfidReader = rfidReader;

            //Subscriber til events fra RFid reader og Door
            _rfidReader.RfidEvent += RfidDetected; 
            _door.DoorEvent += DoorEventHandler; 
        }

        private void RfidDetected(object sender, RfidEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = e.Id;
                        filelog.LogFile( "Skab låst med RFID: " + e.Id);

                        _state = LadeskabState.Locked;
                        _display.UpdateUserMsg("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                    }
                    else
                    {
                        _display.UpdateUserMsg("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    _display.UpdateUserMsg("Dør er allerede åbnet med et RF-ID");
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (e.Id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        filelog.LogFile("Skab låst op med RFID: " + e.Id);

                        _display.UpdateUserMsg("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.UpdateUserMsg("Forkert RFID tag");
                    }
                    break;
            }
        }

       // Her mangler de andre trigger handlere
        private void DoorEventHandler(object sender, DoorEventArgs e)  //eventuelt handler
        {

	        switch (_state)
	        {
                case LadeskabState.Available: 

	                if (e.DoorState == true)  //Kan åbne door - Ulåst
	                {
		                _state = LadeskabState.DoorOpen;
                        _display.UpdateUserMsg("Tilslut Telefon");
	                }
	                else
	                {
                        _display.UpdateUserMsg("Door cannot close when state is Available");
	                }
	                break;

                case LadeskabState.DoorOpen:
	                if (e.DoorState == false)  // Door er closed og ulåst
	                {
		                _state = LadeskabState.Available; 
                        _display.UpdateUserMsg("Indlæs RFID");
	                }
	                else
	                {
		                _display.UpdateUserMsg("Door cannot open when state = DoorOpen");

                    }
	                break;

                case LadeskabState.Locked:
	                _display.UpdateUserMsg("Door cannot open when state = Locked");
	                break;
	        }
        }

 
    }
}
