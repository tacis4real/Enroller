using System;
using System.Drawing;
using System.Windows.Forms;

namespace BioEnumerator.FrameworkControls
{
    public partial class uclDisplayLabel : UserControl
    {
        public enum MessageType
        {
            Success,
            Failure
        }

        private int _messageTimeout;
        public Action<bool> CloseMyContainer;
        public Action<bool> OpenMyContainer;
        public uclDisplayLabel()
        {
            InitializeComponent();
            message_timer.Tick += message_timer_Tick;
            Visible = false;
        }

        void message_timer_Tick(object sender, EventArgs e)
        {
            if (_messageTimeout < 200)
            {
                _messageTimeout++;
                return;
            }
            if (_messageTimeout != 200) return;
            lblMessage.Text = "";
            if (CloseMyContainer != null)
            {
                CloseMyContainer(true);
            }
            Visible = false;
            message_timer.Stop();
            _messageTimeout = 0;
        }

        public void displayMessage(string message, MessageType messageType)
        {
            _messageTimeout = 0;
            switch (messageType)
            {
                case MessageType.Failure:
                     lblMessage.BackColor = Color.Red;
                     lblMessage.ForeColor = Color.Azure;
                     break;
                case MessageType.Success:
                     lblMessage.ForeColor = Color.Azure;
                     lblMessage.BackColor = Color.Green;
                    break;
            }
            lblMessage.Text = message;
            Visible = true;
            if (OpenMyContainer != null)
            {
                OpenMyContainer(true);
            }
            _messageTimeout = 0;
            message_timer.Start();
        }
    }
}
