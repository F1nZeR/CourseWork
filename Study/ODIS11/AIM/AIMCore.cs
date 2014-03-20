using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginFramework;
using System.IO;

namespace ODIS.AIM
{
    /// <summary>
    /// Глобальные настройки
    /// </summary>
    public class AIMCore
    {
        public static int DigitCounts = 5;
        public static string DoubleFormatType = "G";
        public static string DoubleFormat
        {
            get { return ":" + DoubleFormatType + DigitCounts; }
        }

        /// <summary>
        /// Сохранение настроек
        /// </summary>
        public static void Save()
        {
           
        }

        // еще и загрузку сделать
        public static void Load(string AppPath)
        {
            LoadBaseGenerators(AppPath);
            LoadDistributions(AppPath);
            LoadRandomProcesses(AppPath);
            LoadRandomEventStreams(AppPath);
        }

        #region Базовые датчики  

        // Загрузка библиотек с базовыми датчиками
        const string BaseGeneratorsPath = "BaseGenerators";

        public static List<IBaseGeneratorFactory> BaseGeneratorFactories;
        public static IBaseGeneratorFactory CurrentBaseGeneratorFactory = null;

        static void LoadBaseGenerators(string AppPath)
        {
            BaseGeneratorFactories = PluginLoader.GetPlugins<IBaseGeneratorFactory>(Path.Combine(AppPath, BaseGeneratorsPath));

            // создаем встроенный базовый датчик, принудительно добавляем его в список и делаем текущим!
            SimpleBaseGeneratorFactory F = new SimpleBaseGeneratorFactory();
            BaseGeneratorFactories.Add(F);
            CurrentBaseGeneratorFactory = F;
        }

        /// <summary>
        /// Создает и возвращает указатель на базовый датчик, созданный в соответствии с настройками программы
        /// </summary>
        /// <returns></returns>
        public static BaseGenerator GetBaseGenerator()
        {
            return CurrentBaseGeneratorFactory.CreateGenerator();
        }

        #endregion

        #region Случайные величины

        const string DistributionsPath = "";

        public static List<IDistributionFactory> DistributionFactries;

        static void LoadDistributions(string AppPath)
        {
            DistributionFactries = PluginLoader.GetPlugins<IDistributionFactory>(Path.Combine(AppPath, DistributionsPath));
        }

        public static RandomDistribution CreateDistribution(string ClassName, params object[] args)
        {
            foreach (IDistributionFactory df in DistributionFactries)
                if (df.GetType().Name.ToUpper() == (ClassName + "Factory").ToUpper()) return df.CreateDistribution(args);
            return null;
        }

        #endregion 

        #region Случайные процессы

        const string RandomProcessesPath = "";

        public static List<IRandomProcessFactory> RandomProcessFactories;

        static void LoadRandomProcesses(string AppPath)
        {
            RandomProcessFactories = PluginLoader.GetPlugins<IRandomProcessFactory>(Path.Combine(AppPath, RandomProcessesPath));
        }

        public static RandomProcess CreateProcess(string ClassName, params object[] args)
        {
            foreach (IRandomProcessFactory pf in RandomProcessFactories)
                if (pf.GetType().Name.ToUpper() == (ClassName + "Factory").ToUpper()) return pf.CreateProcess(args);
            return null;
        }

        #endregion

        #region Потоки случайных событий

        const string RandomEventStreamsPath = "";

        public static List<IRandomEventStreamFactory> RandomEventStreamFactories;

        static void LoadRandomEventStreams(string AppPath)
        {
            RandomEventStreamFactories = PluginLoader.GetPlugins<IRandomEventStreamFactory>(Path.Combine(AppPath, RandomEventStreamsPath));
        }

        public static RandomEventStream CreateEventStream(string ClassName, params object[] args)
        {
            foreach (IRandomEventStreamFactory sf in RandomEventStreamFactories)
                if (sf.GetType().Name.ToUpper() == (ClassName + "Factory").ToUpper()) return sf.CreateEventStream(args);
            return null;
        }

        #endregion

    }
}
