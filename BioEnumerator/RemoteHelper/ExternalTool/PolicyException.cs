using System;
using System.Text;

namespace BioEnumerator.RemoteHelper.ExternalTool
{
    [Serializable]
    public class PolicyException
    {
        public string MessageId;
        public string Text;
        public string[] Variables;

        public PolicyException()
        {
        }

        public PolicyException(string messageId, string text, string[] variables)
        {
            MessageId = messageId;
            Text = text;
            Variables = variables;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("messageId = ");
            stringBuilder.Append(MessageId);
            stringBuilder.Append(", text = ");
            stringBuilder.Append(Text);
            stringBuilder.Append(", variables = {");
            if (Variables != null)
            {
            for (int index = 0; index < Variables.Length; ++index)
            {
                stringBuilder.Append("[");
                stringBuilder.Append(index);
                stringBuilder.Append("] = ");
                stringBuilder.Append(Variables[index]);
            }
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
