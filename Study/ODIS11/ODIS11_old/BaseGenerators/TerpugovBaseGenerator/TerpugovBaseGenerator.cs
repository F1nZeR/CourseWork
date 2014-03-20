using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ODIS.AIM
{
    /// <summary>
    /// Мультипликативный датчик Терпугова
    /// </summary>
    public class TerpugovBaseGenerator : BaseGenerator
    {
        const int IA = 843314861;
        const int C = 453816693;
        const int IM2 = 1073741824; // Величина М=2147483648 пополам
        private int y;
        public TerpugovBaseGenerator()
        {
            Thread.Sleep(100);
            this.y = Convert.ToInt32(DateTime.Now.Millisecond * 9999); //в начале задержка затем чтоб получилось число побольше :)
        }

        // получить новое значение. частный случай мультипликативного датчика (IA*y +C) mod M
        public override double NextValue()
        {
            // в оригинале для Паскаля были отключены {$O-,$R-} {Optimization- & Range Check-. может здесь не надо?
            y = (y * IA + C);
            if (y < 0) y = (y + IM2) + IM2;
            return (Convert.ToDouble(y)) * 0.4656613E-09;
        }
    }

    public class TerpugovBaseGeneratorFactory : IBaseGeneratorFactory
    {
        #region IBaseGeneratorFactory Members

        public override string ToString()
        {
            return "Терпугов";
        }

        public BaseGenerator CreateGenerator()
        {
            return new TerpugovBaseGenerator();
        }

        #endregion
    }
}
