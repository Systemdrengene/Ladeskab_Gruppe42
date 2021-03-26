﻿using NUnit.Framework;
using Ladeskab.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;


namespace Ladeskab.Lib.Test
{
	[TestFixture()]
	public class ChargeControlUnitTest
	{
		//Pre-Setup
		private ChargeControl _uut;
		private IDisplay _display;
		private IUsbCharger _mockUsbCharger;

		[SetUp]
		public void Setup()
		{
			_display = Substitute.For<IDisplay>();
			_mockUsbCharger = Substitute.For<IUsbCharger>();

			_uut = new ChargeControl(_display, _mockUsbCharger);
		}

		#region IsConnectedTests
		[Test()]
		public void IsConnected_Connected_IsTrue()
		{
			//Arrange

			// Act
			_mockUsbCharger.Connected.Returns(true);

			//Assert
			Assert.IsTrue(_uut.IsConnected());
		}

		[Test()]
		public void IsConnected_Connected_IsFalse()
		{
			//Arrange

			// Act
			_mockUsbCharger.Connected.Returns(false);

			//Assert
			Assert.IsFalse(_uut.IsConnected());
		}
		#endregion

		#region StartChargeTests

		[Test()]
		public void StartCharge_UsbChargeStart_CallOnce()
		{
			//Arrange

			//Act
			_uut.StartCharge();
			//Assert
			_mockUsbCharger.Received(1).StartCharge();
		}


		#endregion

		#region StopChargeTests

		[Test]
		public void StopCharge_UsbChargeStop_CallOnce()
		{
			//Arrange

			//Act
			_uut.StopCharge();

			//Assert
			_mockUsbCharger.Received(1).StopCharge();
		}

		#endregion

		#region OnChargeCurrentEvent

		[Test]
		public void OnNewCurrent_ListenForCurrent_CurrentIsReceived()
		{
			//Arrange

			//Act
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() {Current = 100.000});

			//Assert
			Assert.That(_uut.ReadChargeCurrent,Is.EqualTo(100.000));
		}


		#endregion

		#region EvalCurrentTestCases

		[TestCase(0.000, 0)]
		[TestCase(0.001, 1)]
		[TestCase(5.000, 1)]
		[TestCase(5.001, 2)]
		[TestCase(500.000, 2)]
		[TestCase(500.001, 3)]
		public void EvalCurr_CurrrentCharge_ChargeStateIsCorrect(double currCharge, int chargeState)
		{
			//Arrange

			//Act
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() {Current = currCharge});
			//Assert
			Assert.That(_uut.ReadChargerState,Is.EqualTo(chargeState));
		}

		[Test]
		public void EvalCurr_OverCurrFail_StopChargeCall()
		{
			//Arrange

			//Act
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() {Current = 700.000});
			//Assert
			_mockUsbCharger.Received(1).StopCharge();
		}

		[Test]
		public void EvalCurrent_OverCurrFail_PersistDisplay()
		{
			//Arrange

			//Act
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() {Current = 700.000});
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() { Current = 0.000 });
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() { Current = 0.000 });

			//Assert
			Assert.That(_uut.ReadChargerState,Is.EqualTo((int)3));
		}

		#endregion

		#region UpdateDisplay

		[TestCase(2.5, "Fully Charged")] 
		[TestCase(25.0, "Is Charging")] 
		[TestCase(525.0, "Current failed")] 
		public void UpdateDisplay_ChangeState_CalledOnceNoIdle(double testCurr, string m)
		{
			//Arrange

			// Act
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() { Current = testCurr });
			Console.WriteLine(_mockUsbCharger.CurrentValue);

			//Assert
			_display.Received(1).UpdateChargeMsg(m); 

		}

		[TestCase( 0.0, "Idle")]  
		[TestCase( 2.5,"Fully Charged") ] 
		[TestCase( 25.0,"Is Charging") ] 
		[TestCase( 525.0,"Current failed")] 
		public void UpdateDisplay_Events_CalledOnceExceptIdle
			( double testCurrent, string m)
		{
			// Arrange
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() {Current = 20});
			// Act
	
			_mockUsbCharger.CurrentValueEvent +=
					Raise.EventWith(new CurrentEventArgs() { Current = testCurrent });

			// Assert
			_display.Received(1).UpdateChargeMsg(m);
	
		}


		[Test]
		public void UpdateDisplay_ChargerGoesFromChargingToIdle_ChargerStateIdle()
		{
			// Arrange

			// Act
			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() { Current = 300.000 });

			_mockUsbCharger.CurrentValueEvent +=
				Raise.EventWith(new CurrentEventArgs() { Current = 0.000 });


			// Assert
			Assert.That(_uut.ReadChargerState, Is.EqualTo((int)0));
		}


		#endregion

	}
}
