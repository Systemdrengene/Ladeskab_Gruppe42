using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace Ladeskab.Lib
{
	class ChargeControl
	{
		private IUsbCharger _usbCharger;
		public event EventHandler<CurrentEventArgs> CurrentValueEvent;
		bool IsConnected()
		{
			if(_usbCharger.Connected)
				return false;
			else
			{ 
				return true;
			}
		}

		void StartCharge()
		{
			CurrentValueEvent.Invoke(this,new CurrentEventArgs());
		}

		void StopCharge()
		{
			CurrentValueEvent.Invoke(this, new CurrentEventArgs());
		}

	}
}
