//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc. All rights reserved.
// Written by:	Alex Novitskiy
//-------------------------------------------------------------------

// Summary:
//      Shared definitions for interface IDynoCardRead and its clients
//      Namespace: SNC.OptiRamp.Services.RodPump
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SNC.OptiRamp.Services.fRT;
using SNC.OptiRamp.Services.fDefs;

namespace SNC.OptiRamp.Services.RodPump
{
    /// <summary>
    ///     Runtime data
    ///     Used as input and output for Rodpump computation component
    /// </summary>
    public class DynoCardRTData
    {
        public DateTime timeStampUTC { get; set; }
        public string   pumpID       { get; set; }      // for example "4E0406A"
        public string   dataFile     { get; set; }      // for example "4E0406A_09172013.DYN"
        
        /// <summary>
        ///     Dynocard map data
        /// </summary>
        public IEnumerable<Point> DataItems { get; set; }
        /// <summary>
        ///     Collection of RT Values
        /// </summary>
        public IEnumerable<RTValue> RTData { get; set; }
        /// <summary>
        ///     Dictionary of rod pump configuration values
        /// </summary>
        public Dictionary<string, double> configValues { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface MotorDynamicsComputer
    {
        /// <summary>
        ///     returns angle of motor [0....2*Pi[
        /// </summary>
        double Angle(DateTime timeUTC);
        /// <summary>
        /// This function computes position and Dpstroke(true)/Downstroke(false) state
        /// Position is in range [0 ... 100] %
        /// 0 --> 100 == Upstroke
        /// 100 --> 0 == Downstroke
        /// Angle (rad) | Position (%)
        /// 0           | 0
        /// 0.5*PI      | 50
        /// PI          | 100
        /// 1.5*PI      | 50
        /// 2 * PI      | 0
        /// </summary>
        /// <param name="timeUTC"></param>
        /// <param name="position"></param>
        /// <param name="upstroke"></param>
        void Position(DateTime timeUTC, out double position, out bool upstroke);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface DynocardComputation
    {
        /// <summary>
        /// 
        ///     returns error or (null and value)
        /// </summary>
        string MotorDynamicsMinTimeRange(double tn, ref double MinTimeRangeSec);

        
        
        /// <summary>
        /// 
        ///     returns null in case of error
        /// </summary>
        MotorDynamicsComputer MotorDynamics
        (
            double tn, // rotation speed (seconds), for example 6
            IEnumerable<RTDataDouble> data, // position data
            ref bool Stop, 
            out string Error
        );

        /// <summary>
        /// 
        ///     returns null in case of error
        /// </summary>
        IEnumerable<Point> MotorDynaCard
        (
            double tn,
            IEnumerable<RTDataDouble> dataPosition,
            IEnumerable<RTDataDouble> dataTorque,
            ref bool Stop,
            out string Error
        );
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class RodPumpDefinitions
    {
        /// <summary>
        /// const string  - names of properties and io
        /// </summary>
        // Pump off config parameters labels
        public const string txtLowFillageSetPoint = "Fillage Low SP";
        public const string txtTravelCountBeforeStop = "Number of Travels Before Stop";
        public const string txtPumpOffPosition = "Pump Off Position";
        public const string txtDownTimeSetPoint = "Down Time SP";
        public const string txtShutdownCmnd = "Shut Down Command, RP";
        public const string txtStartCmnd = "Start Command, RP";
        public const string txtNominalStrokingTime = "tn";                 // "Nominal Stroking Time" sec - property
        public const string txtNodPumpFillage = "Fillage";                 // "Fillage"- calculated fillage
        public const string txtRodPumpPosition = "Rod Position, RP";       // "Rod Position" - Analog Input signal
        public const string txtWellheadPressure = "Wellhead Pressure, RP"; // "Wellhead Pressure, RP" - Analog Input signal
        public const string txtWellheadPressureForDynocard = "Pwh";        // "Pwh" - Analog signal for dynocard computation
        public const string txtRodPumpTorque = "Torque, RP";               // "Rod Position" - Analog Input signal
        public const string txtOnOff = "On/Off, RP";                       // "On/Off, RP" - Digital Input/Output signal
        public const string txtRunStop = "RunStop";
        public const string txtAutoManual = "AutoManual";

    }

    /// <summary>
    /// Implementation of IDataChannel for return of algorithm output values.
    /// </summary>
    public class OutputChannel : IDataChannel
    {
        #region Private Fields
        #endregion Private Fields

        #region Public Interface
        public OutputChannel(string d, string n, string c, double? min, double? max, IProjectEU eu)
        {
            Description = d;
            Name = n;
            CustomerTag = c;
            Min = min;
            Max = max;
            EU = eu;
        }
        #endregion Public Interface

        #region Properties
        public string Description { get; set; }
        public string Name { get; set; }
        public string CustomerTag { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
        public IProjectEU EU { get; set; }
        public bool NCUType { get; set; }
        public bool DiscreteType { get; set; }
        #endregion Properties

        #region Private Implementation
        #endregion Private Implementation
    }
} // ............................................ namespace SNC.OptiRamp.Services.RodPump ..................................
