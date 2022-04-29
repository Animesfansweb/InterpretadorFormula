using DynamicExpresso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpretadorFormula
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal valor = 0.00m;
            string formula = "(@SUELDONOM/30) * @DIASPAG";

           

            ValoresRolPago rp = new ValoresRolPago();
            rp.DIASPAGADOS = 10;
            rp.SUELDONOMINAL = 400;

            Console.WriteLine("DIASPAGADOS: " + rp.DIASPAGADOS);
            Console.WriteLine("SUELDONOMINAL: " + rp.SUELDONOMINAL);
            Console.WriteLine(formula);

            SetVariables obj = new SetVariables();
            SetVariablesFormulas(obj, rp);


            if (!string.IsNullOrEmpty(formula))
            {

                double res = Interpretador.Compilar(obj, formula);
                valor = Math.Round(Convert.ToDecimal(res), 2);

                if (valor == -1)
                    valor = 0.00m;
            }

            Console.WriteLine(valor);
            Console.ReadKey();

        }



        private static void SetVariablesFormulas(SetVariables obj, ValoresRolPago Valores_Rol_Pago)
        {
            obj.DIASPAG = Convert.ToDouble(Valores_Rol_Pago.DIASPAGADOS);
            obj.SUELDONOM = Convert.ToDouble(Valores_Rol_Pago.SUELDONOMINAL);

        }
        public class ValoresRolPago
        {
            public Nullable<int> DIASPAGADOS { get; set; }
            public Nullable<decimal> SUELDONOMINAL { get; set; }
        }
        // Define other methods and classes here
        public class SetVariables
        {
            protected double validaDouble(double value)
            {
                double doub = 0f;
                doub = (value > 0) ? value : 0;
                return value;
            }


            //SUELDO NOMINAL
            private double _SUELDONOM = 0f;
            public double SUELDONOM
            {
                get { return _SUELDONOM; }
                set { _SUELDONOM = validaDouble(value); }
            }

            //DIASACLNP
            private double _DIASPAG = 0;
            public double DIASPAG
            {
                get { return _DIASPAG; }
                set { _DIASPAG = value; }
            }


        }


        public static class Interpretador
        {
            public static double Compilar(SetVariables vr, string formula)
            {
                double result = 0f;

                try
                {
                    if (!string.IsNullOrEmpty(formula))
                    {
                        var interpreter = new Interpreter().SetVariable("obj", vr);

                        string expression = formula.Replace("@", " obj.");

                        var total = interpreter.Eval(expression);

                        result = double.Parse(total + "") + 0f;
                    }

                }
                catch (Exception e)
                {
                    result = -1 + 0f; // si es -1 es error no controlado
                }

                return result;
            }


        }
    }
}
