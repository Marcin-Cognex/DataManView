using System.Windows.Forms;

namespace DataManView
{
    /// <summary>
    /// Double Buffered layout panel - removes flicker during resize operations.
    /// </summary>
    public partial class DBLayoutPanel : TableLayoutPanel
    {
        public DBLayoutPanel()
        {
            this.DoubleBuffered = true;
        }
    }
}