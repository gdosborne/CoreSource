using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Support.FormatCSFile
{
    public delegate void StartProcessingHandler(object sender, StartProcessingEventArgs e);
    public class StartProcessingEventArgs : EventArgs
    {
    }
}
