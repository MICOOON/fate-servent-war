using System.Collections;
using System.Collections.Generic;

public class SocketModel
{
    // 一级协议, 用于区分所属模块
    public byte type { get; set; }
    // 二级协议, 用于区分模块下的子模块
    public int area { get; set; }
    // 三级协议, 用于区分当前处理的逻辑
    public int command { get; set; }
    // 消息体, 当前需要处理的主体数据
    public object message { get; set; }

    public SocketModel() {

    }

    public SocketModel(byte type, int area, int command, object message) {
        this.type = type;
        this.area = area;
        this.command = command;
        this.message = message;
    }

    public T GetMessage<T>() {
        return (T)message;
    }
}
