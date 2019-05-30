namespace ISAACS
{
    using UnityEngine;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class WriteText : MonoBehaviour
    {
        void Start()
        {
            WriteToFile();
        }

        void Update()
        {
        }

        public static void WriteToFile()
        {
            StreamWriter sw = new StreamWriter(Application.persistentDataPath + "table.txt");

            sw.WriteLine("Generated table of 1 to 10");
            sw.WriteLine("");

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    sw.WriteLine("{0}x{1}= {2}", i, j, (i * j));
                }

                sw.WriteLine("====================================");
            }

            sw.WriteLine("Table successfully written to file!");

            sw.Close();
        }
    }
}