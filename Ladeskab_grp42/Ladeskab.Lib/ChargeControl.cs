using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{

	public interface IChargeControl
	{
		void StartCharge();
		void StopCharge();
		bool IsConnected();
	}

	public class ChargeControl : IChargeControl
	{
		//Forbindelser
		private readonly IUsbCharger _usbCharger;
		private readonly IDisplay _display;
		//States
		private enum ChargerState
		{
			Idle,
			FullyCharged,
			IsCharging,
			OverCurrentFail
		};

		//Ladesituationer 
		private ChargerState _chargerState = ChargerState.Idle;
		private ChargerState _latestState = ChargerState.Idle;
		public int ReadChargerState = -1;

		//Max_min værdier for charging current
		private const double MaximumChargeCurrent = 500.000;
		private const double MinimumChargeCurrent = 5.000;
		private const double ZeroChargeCurrent = 0.000;
		//attributter
		private double _chargeCurrent = 0.0;
		public double ReadChargeCurrent = -1.0;

		public ChargeControl(IDisplay display, IUsbCharger usbCharger)
		{
			//Use Display
			_display = display;
			// Use USBCharger
			_usbCharger = usbCharger;
			//Attach to events fra USB charger
			this._usbCharger.CurrentValueEvent += OnChargeCurrUpdate;

		}

		public bool IsConnected()
		{
			return _usbCharger.Connected;
		}

		public void StartCharge()
		{
			_chargerState = ChargerState.Idle;
			_usbCharger.StartCharge();

		}

		public void StopCharge()
		{
			_chargerState = ChargerState.Idle;
			_usbCharger.StopCharge();

		}

		private void OnChargeCurrUpdate(object sender, CurrentEventArgs a)
		{
			_chargeCurrent = a.Current;
			ReadChargeCurrent = _chargeCurrent;

			EvalChargeState();
			UpdateDisplay();
		}

		//EvalChargeState
		private void EvalChargeState()
		{
			if (_chargeCurrent > MaximumChargeCurrent) // chargeCurrent > 500.00 mA
			{
				_usbCharger.StopCharge();
				_chargerState = ChargerState.OverCurrentFail;  // 3
			}
			else if (_chargeCurrent <= MaximumChargeCurrent 
			         && _chargeCurrent > MinimumChargeCurrent) // 500 mA > chargecurrent > 5 mA
			{
				_chargerState = ChargerState.IsCharging;  //1
			}
			else if (_chargeCurrent > ZeroChargeCurrent 
			         && _chargeCurrent <= MinimumChargeCurrent) // Fully charged
			{
				_chargerState = ChargerState.FullyCharged; // 2
			}
			else
			{
				if (_chargerState == ChargerState.OverCurrentFail) return;  

				_chargerState = ChargerState.Idle;  // 0
			}

			ReadChargerState = (int)_chargerState;

		}
		private void UpdateDisplay()
		{
			if (_latestState == _chargerState)
			{
				return;
			}
			switch (_chargerState)
			{
				case ChargerState.OverCurrentFail:   //State charge fail
					_display.UpdateChargeMsg("Current failed");
					_latestState = ChargerState.OverCurrentFail;
					break;
				case ChargerState.FullyCharged:
					_display.UpdateChargeMsg("Fully Charged");
					_latestState = ChargerState.FullyCharged;
					break;
				case ChargerState.IsCharging:
					_display.UpdateChargeMsg("Is Charging");
					_latestState = ChargerState.IsCharging;
					break;
				case ChargerState.Idle:
					_display.UpdateChargeMsg("Idle");
					_latestState = ChargerState.Idle;
					break;
				default:
					break;
			}
		}

	}
}
