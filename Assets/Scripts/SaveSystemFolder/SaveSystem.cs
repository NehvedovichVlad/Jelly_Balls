using Assets.Srcipts.Menu;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Srcipts.SaveSystemFolder
{
    public static class SaveSystem 
    {
        public static readonly string Dir = "/SaveData/";
        public static readonly string FileName = "progress";
        public static void Save(Progress progress)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(GetPath(), FileMode.Create);
            ProgressData progressData = new ProgressData(progress);
            binaryFormatter.Serialize(fileStream, progressData);
            fileStream.Close();
        }

        public static ProgressData Load()
        {
            string path = GetPath();
            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(path, FileMode.Open);
                ProgressData progress = binaryFormatter.Deserialize(fileStream) as ProgressData;
                fileStream.Close();
                return progress;
            }
            else
            {
                Debug.Log("Mistakes");
                return null;
            }
        }

        public static void DeleteFile() => File.Delete(GetPath());
        private static string GetPath() => $"{Application.persistentDataPath}/progress.xx";



        // 2 вариант сохранения для более сложных вещей
        // выключить конструктор в прогрессдата  и в прогрессе добавить progressData

        public static void SaveNewtonSoft(ProgressData progress)
        {
            string dir = Application.persistentDataPath + Dir;
            string jsonString = JsonConvert.SerializeObject(progress);
            File.WriteAllText(dir + FileName, jsonString);

        }
        public static ProgressData LoadNewtonSoft()
        {
            string path = Application.persistentDataPath + Dir + FileName;
            ProgressData progress = new(null); // убрать null при использовании
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                progress = JsonConvert.DeserializeObject<ProgressData>(json);
                return progress;
            }
            return null;
        }
        
    }
    }



