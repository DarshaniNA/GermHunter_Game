
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class PlayerPrefsUtil
{
    public static void SavePlayer(PlayerObject player)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerObject));
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, player);
            PlayerPrefs.SetString("player values", sw.ToString());
        }
    }

    public static PlayerObject LoadPLayer()
    {
        PlayerObject playerValues;
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerObject));
        string text = PlayerPrefs.GetString("player values");
        if (text.Length == 0)
        {
            playerValues = new PlayerObject();
        }
        else
        {
            using (var reader = new StringReader(text))
            {
                playerValues = serializer.Deserialize(reader) as PlayerObject;
            }
        }
        return playerValues;
    }

}