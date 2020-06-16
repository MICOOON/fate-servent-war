using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ByteArray {
    MemoryStream ms = new MemoryStream();
    BinaryReader br;
    BinaryWriter bw;

    public ByteArray() {
        br = new BinaryReader(ms);
        bw = new BinaryWriter(ms);
    }

    public ByteArray(byte[] buff) {
        ms = new MemoryStream(buff);
        br = new BinaryReader(ms);
        bw = new BinaryWriter(ms);
    }

    public byte[] GetBuffer() {
        byte[] result = new byte[ms.Length];
        Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int) ms.Length);
        return result;
    }

    public void Read(out int value) {
        value = br.ReadInt32();
    }

    public void Read(out byte value) {
        value = br.ReadByte();
    }

    public void Read(out byte[] value, int length) {
        value = br.ReadBytes(length);
    }

    public void Write(byte value) {
        bw.Write(value);
    }

    public void Write(int value) {
        bw.Write(value);
    }

    public void Write(byte[] value) {
        bw.Write(value);
    }

    public bool Readable {
        get {
            return ms.Length > ms.Position;
        }
    }

    public int Length {
        get {
            return (int) ms.Length;
        }
    }

    public int Position {
        get {
            return (int) ms.Position;
        }
    }

    public void Close() {
        br.Close();
        bw.Close();
        ms.Close();
    }
}
