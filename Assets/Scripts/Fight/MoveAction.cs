using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Fight {
    class MoveAction : IAction {
        private byte _type;
        private int _area;
        private int _command;
        private object _message;

        public MoveAction(byte type, int area, int command, object message) {
            _type = type;
            _area = area;
            _command = command;
            _message = message;
        }

        public void ProcessAction() {
            NetIO.Instance.Write(_type, _area, _command, _message);
        }
    }
}
