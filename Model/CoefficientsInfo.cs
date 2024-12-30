using AreaCalculationPlugin.Calculator;
using System.Collections.Immutable;

namespace AreaCalculationPlugin.Model;

public class CoefficientsInfo
{
    private double coefficient;
    public double Coefficient
    {
        get
        {
            return coefficient;
        }
        set
        {
            if (Math.Round(value, 2) <= 0 || value > 1)
                throw new ArgumentException("Недопустимое значение");

            coefficient = value;
        }
    }

    public readonly string NameRoomCategory; 

    public CoefficientsInfo(string nameRoomCategory, double coefficient)
    {
        NameRoomCategory = nameRoomCategory;
        Coefficient = coefficient;
    }
}
