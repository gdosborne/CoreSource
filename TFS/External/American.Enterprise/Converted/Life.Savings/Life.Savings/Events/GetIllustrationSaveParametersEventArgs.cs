using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Life.Savings.Events
{
    public enum SaveFileTypes { Xps, Pdf }
    public delegate void GetIllustrationSaveParametersHandler(object sender, GetIllustrationSaveParametersEventArgs e);
    public class GetIllustrationSaveParametersEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public bool IsCancel { get; set; }
        public FlowDocument Document { get; set; }
        public SaveFileTypes FileType { get; set; }
    }
}
