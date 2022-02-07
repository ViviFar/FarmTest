using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveStateMachine(StateMachine machine)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/statemachine.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(machine);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("saved at " + path);
    }

    public static PlayerData LoadDatas()
    {
        string path = Application.persistentDataPath + "/statemachine.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("no file with such name: " + path);
            return null;
        }
    }
}
