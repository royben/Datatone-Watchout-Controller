using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sirilix.WatchoutController.TestApp
{
    public class SettingsManager
    {
        /// <summary>
        /// Saves the specified to file.
        /// </summary>
        /// <param name="toFile">To file.</param>
        public static void Save(SettingsModel model, String toFile)
        {
            BinaryFormatter f = new BinaryFormatter();

            using (FileStream fs = new FileStream(toFile, FileMode.Create))
            {
                f.Serialize(fs, model);
            }
        }

        /// <summary>
        /// Loads the specified from file.
        /// </summary>
        /// <param name="fromFile">From file.</param>
        public static SettingsModel Load(String fromFile)
        {
            SettingsModel model = new SettingsModel();

            try
            {
                BinaryFormatter f = new BinaryFormatter();
                using (FileStream fs = new FileStream(fromFile, FileMode.Open))
                {
                    model = f.Deserialize(fs) as SettingsModel;
                }

                return model;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return new SettingsModel();
            }
        }
    }
}
