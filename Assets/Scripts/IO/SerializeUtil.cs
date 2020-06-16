using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializeUtil
{
    // 序列化对象
    public static byte[] Encode(object value) {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(ms, value);
        byte[] result = new byte[ms.Length];
        Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int) ms.Length);
        ms.Close();
        return result;
    }

    // 反序列化对象
    public static object Decode(byte[] value) {
        MemoryStream ms = new MemoryStream(value);
        BinaryFormatter bf = new BinaryFormatter();
        object result = bf.Deserialize(ms);
        ms.Close();
        return result;
    }
}
