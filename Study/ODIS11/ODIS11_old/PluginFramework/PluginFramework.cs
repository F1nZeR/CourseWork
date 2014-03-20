using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace PluginFramework
{

    /// <summary>
    /// Загрузчик плагинов
    /// </summary>
    public class PluginLoader
    {
        /// <summary>
        /// Загрузка плагинов
        /// </summary>
        /// <typeparam name="T">Тип интерфейса(в этом примере - iPlugin)</typeparam>
        /// <param name="folder">Путь к папке с плагинами</param>
        /// <returns>Список объектов, реализующих интерфейс плагина</returns>
        public static List<T> GetPlugins<T>(string folder)
        {
            //Создаем контейнер под результат
            List<T> ResultList = new List<T>();
            //Узнаем абсолютные пути ко всем длл-кам в папке плагинов
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string f in files)
            {
                //try
                //{
                
                    //подгружаем длл-ку
                    Assembly assembly = Assembly.LoadFile(f);

                    //выдергиваем интерфейсы всех паблик классов в длл-ке
                    foreach (Type t in assembly.GetTypes())
                    {
                        if (!t.IsClass || t.IsNotPublic) continue;
                        Type[] interfaces = t.GetInterfaces();

                        //если интерфейс совпадает с нашим интерфейсом плагинов, инстанцируем его и кладем в Результат
                        if (((IList<Type>)interfaces).Contains(typeof(T)))
                        {
                            object obj = Activator.CreateInstance(t);
                            ResultList.Add((T)obj);
                        }
                    }
                //}
                //catch
                //{
                    
                //}
            }            

            return ResultList;
        }
    }
}
