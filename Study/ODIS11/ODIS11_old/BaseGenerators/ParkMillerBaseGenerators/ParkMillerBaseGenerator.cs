using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ODIS.AIM
{
    /// <summary>
    /// Мультипликативный датчик Park-Miller
    /// </summary>
    public class ParkMillerBaseGenerator : BaseGenerator
    {
        // другие значения 
        // в общей формуле для мультипликативных датчиков ((IA*Yi) mod M), M=AI*IQ+IR
        // и теперь t = IA*(y mod IQ)-IR*[y/IQ] если t<0, то t += M.
        protected const int IA = 16807;
        protected const int IM = 2147483647;
        protected const double AM = 1.0 / IM; // 0.46566128752458E-09;
        protected const int IQ = 127773;
        protected const int IR = 2836;
        protected int y;
        
        public ParkMillerBaseGenerator()
        {
            Thread.Sleep(100);
            this.y = DateTime.Now.Millisecond * 9999;
        }

        // Вычисляем целочисленный у по формуле
        protected int GetValue()
        {
            int k;
            k = y / IQ; // к целая часть от деления
            this.y = IA * (y - k * IQ) - IR * k; // хитро вычисляется остаток от деления (mod) %?
            if (y < 0) y += IM;
            return (y);
        }

        public override double NextValue()
        {
            return (AM * GetValue()); // получаем распределение от 0 до 1
        }
    }

    public class ParkMillerBaseGeneratorFactory: IBaseGeneratorFactory
    {
        #region IBaseGeneratorFactory Members

        public override string ToString()
        {
            return "Парк-Миллер";
        }

        public BaseGenerator CreateGenerator()
        {
            return new ParkMillerBaseGenerator();
        }

        #endregion
    }

    /// <summary>
    /// Мультипликативный датчик Park-Miller с перестановками
    /// </summary>
    public class BaysDurhamRandomGenerator : ParkMillerBaseGenerator
    {
        const int NTAB = 32; // размерность массива
        const int NDIV = (1 + (IM - 1) / NTAB); // величина для устранения корреляции между сериями сгенерированных случайных чисел
        const double EPS = 1.2e-7; //
        const double RNMX = (1.0 - EPS);
            
        public BaysDurhamRandomGenerator() : base() { }

        public override double NextValue()
        {
            int iy = 0; int j;
            int[] iv = new int[NTAB];
            if ((this.y <= 0) || (iy == 0)) // в сишном коде было if(dummy<=0 || !iy) {... но тогда надо чтоб начально значение было отрицательным
            {
                if (this.y < 0) this.y = -this.y; else { if (this.y == 0) this.y = 1; }
                for (j = NTAB + 7; j >= 0; j--) /// отбрасываем первые элементы, чтоб повысить точность
                    if (j < NTAB) iv[j] = GetValue();
                iy = iv[0];
            }
            iy = iv[j = iy / NDIV]; iv[j] = GetValue();
            double tmp;
            if ((tmp = AM * iy) > RNMX) return (RNMX);
            else return (tmp);
        }

        ///думаю еще сделать Алгоритм Л'Экюера или не надо?
    }

    public class BaysDurhamBaseGeneratorFactory : IBaseGeneratorFactory
    {

        #region IBaseGeneratorFactory Members

        public override string ToString()
        {
            return "Парк-Миллер с перестановкками";
        }

        public BaseGenerator CreateGenerator()
        {
            return new BaysDurhamRandomGenerator();
        }

        #endregion
    }
    
}

