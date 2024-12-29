using AreaCalculationPlugin.Model;
using AreaCalculationPlugin.View;
using System.Resources;
using Teigha.Runtime;

namespace AreaCalculationPlugin.Calculator
{
    public class PluginManager
    {
        [CommandMethod("AreaCalc")]
        public static void Start()
        {
            var defaultAreaCoefficients = GetRoomCoefficients();
            var mainForm = new AreaOfPremises(defaultAreaCoefficients);
            RoomData.ResetParameters();
            /*var rooms = DataReader.GetRoomData();*/
            /*mainForm.ChoseRooms += RoomParameterCorrector.PerformCalculations;*/

            HostMgd.ApplicationServices.Application.ShowModalDialog(mainForm);
        }

        private static CoefficientsInfo[] GetRoomCoefficients()
        {
            var rm = new ResourceManager(typeof(AreaOfPremises));

            return rm.GetString("Coefficients").Split("\r\n")
                .Select(line => line.Split(": "))
                .Select(array => new CoefficientsInfo(array.First(), double.Parse(array.Last())))
                .ToArray();
        }
    }
}
