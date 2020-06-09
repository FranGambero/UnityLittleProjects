using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using UnityEditor;

public class SerializerManager : Singleton<SerializerManager> {

    public PlayerData DeserializePlayer() {
        PlayerData playerData;

        playerData = DeSerializePlayer();

        return playerData;
    }
    public PlayerData SerializeBinary(PlayerData playerData) {
        string path = null;
#if UNITY_STANDALONE
        // You cannot add a subfolder, at least it does not work for me
        path = "Persistencia_Data/Resources/PlayerData";
#endif
#if UNITY_EDITOR
        path = "Assets/Resources/PlayerData";
#endif
        // Abrimos el archivo
        FileStream fs = new FileStream(path, FileMode.Create);
        //Declaramos el serializador
        BinaryFormatter formatter = new BinaryFormatter();

        //Serializamos losdatos en el fichero
        formatter.Serialize(fs, playerData);

        //Cerramos el archivo 
        fs.Close();
        return playerData;
    }
    private PlayerData DeSerializePlayer() {
        string path = null;
#if UNITY_STANDALONE
        // You cannot add a subfolder, at least it does not work for me
        path = "Persistencia_Data/Resources/PlayerData";
#endif
#if UNITY_EDITOR
        path = "Assets/Resources/PlayerData";
#endif
        PlayerData playerData = null;
        if (System.IO.File.Exists(path)) {

            FileStream fs = new FileStream(path, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();

            playerData = (PlayerData)formatter.Deserialize(fs);

            fs.Close();
        }

        return playerData;
    }

    public void SerializeXML(PostItList postItList) {
        //string path = EditorUtility.SaveFilePanel("newList", "", "", "xml");
        //StreamWriter writer = new StreamWriter(path);
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        if(!Directory.Exists(path + "/POSTITDATA")) {
            Directory.CreateDirectory(path + "/POSTITDATA");
        }
        StreamWriter writer = new StreamWriter(path + "/POSTITDATA/PostItListXML.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(PostItList));
        serializer.Serialize(writer, postItList);
        writer.Close();
    }

    public PostItList DeserializeXML(string file) {
        FileStream fs = new FileStream(file, FileMode.Open);

        XmlSerializer serializer = new XmlSerializer(typeof(PostItList));

        PostItList postItList = (PostItList)serializer.Deserialize(fs);             

        fs.Close();
        return postItList;

    }
}
