using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetIO
{
    private static NetIO netIO;

    private Socket socket;
    private string ip = "127.0.0.1";
    private int port = 6650;
    private byte[] buff = new byte[1024];
    private List<byte> cache = new List<byte>();
    bool isReading;
    public List<SocketModel> models = new List<SocketModel>();

    private NetIO() {
        try {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, port);

            socket.BeginReceive(buff, 0, 1024, SocketFlags.None, ReceiveCallback, buff);
        } catch (Exception e) {
            Debug.Log(e.Message);
        }
    }

    public static NetIO Instance {
        get {
            if (netIO == null) {
                netIO = new NetIO();
            }
            return netIO;
        }
    }

    private void ReceiveCallback(IAsyncResult ar) {
        try {
            int lenght = socket.EndReceive(ar);
            byte[] msg = new byte[lenght];
            Buffer.BlockCopy(buff, 0, msg, 0, lenght);
            cache.AddRange(msg);

            if (!isReading) {
                isReading = true;
                OnData();
            }

            socket.BeginReceive(buff, 0, 1024, SocketFlags.None, ReceiveCallback, buff);
        } catch (Exception e) {
            Debug.Log("远程服务器连接失败: " + e.Message);
        }
    }

    // 数据解码
    private void OnData() {
        byte[] result = Decode(ref cache);
        if (result == null) {
            isReading = false;
            return;
        }

        SocketModel model = Mdecode(result);
        if (model == null) {
            isReading = false;
            return;
        }

        models.Add(model);

        OnData();
    }

    // 数据长度解码
    private byte[] Decode(ref List<byte> cache) {
        if (cache.Count < 4) {
            return null;
        }

        MemoryStream ms = new MemoryStream(cache.ToArray());
        BinaryReader br = new BinaryReader(ms);
        int length = br.ReadInt32();
        if (length > ms.Length - ms.Position) {
            return null;
        }
        byte[] result = br.ReadBytes(length);

        cache.Clear();

        cache.AddRange(br.ReadBytes((int) (ms.Length - ms.Position)));
        br.Close();
        ms.Close();
        return result;
    }

    // 消息体解码
    private SocketModel Mdecode(byte[] result) {
        ByteArray ba = new ByteArray(result);
        SocketModel model = new SocketModel();
        // 从数据中读取三层协议, 读取数据顺序必须和写入的顺序保持一致
        byte type;
        int area;
        int command;
        ba.Read(out type);
        ba.Read(out area);
        ba.Read(out command);
        model.type = type;
        model.area = area;
        model.command = command;

        // 判断读取完协议后是否还有数据要读取, 是则说明有消息体, 进行消息读取
        if (ba.Readable) {
            byte[] message;
            ba.Read(out message, ba.Length - ba.Position);
            // 反序列化对象
            model.message = SerializeUtil.Decode(message);
        }

        ba.Close();
        return model;
    }

    public void Write(byte type, int area, int command, object message) {
        // 消息体编码
        ByteArray ba = new ByteArray();
        ba.Write(type);
        ba.Write(area);
        ba.Write(command);
        if (message != null) {
            ba.Write(SerializeUtil.Encode(message));
        }

        // 长度编码
        ByteArray ba2 = new ByteArray();
        ba2.Write(ba.Length);
        ba2.Write(ba.GetBuffer());

        // 发送
        try {
            socket.Send(ba2.GetBuffer());
        } catch (Exception e) {
            Debug.Log("网络错误, 请重新登陆" + e.Message);
        }
    }
}
