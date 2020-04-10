using System;
using System.Collections.Generic;

namespace LT.SO.Site.Models
{
    public class ValidationMessage
    {
        public ValidationMessage() { }

        public ValidationMessage(ServiceResult result, string successMsg = null)
        {
            MsgType = result.Success ? 1 : 2;
            Messages = new List<string>();

            if (!result.Success)
            {
                Messages.AddRange(result.Erros);
            }
            else
            {
                Messages.Add(!string.IsNullOrEmpty(successMsg) ? successMsg : Convert.ToString(result.Data));
            }
        }

        public int MsgType { get; set; }

        public List<string> Messages { get; set; }

        public string CallBackUrl { get; set; }
    }
}