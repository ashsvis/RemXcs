using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draws.Plugins
{
    public enum UpdateKind
    {
        UpdateBefore, UpdateAfter
    }

    public delegate void UpdateDraw(Draw element, UpdateKind BeforeAfter);

    public delegate object SelectData(params object[] args);

    public enum SelectDataKind
    {
        DataPoints = 0,
        DataImages = 1,
        DataSchemes = 2
    }

    public interface IDrawPlugin
    {
        Bitmap PluginPicture { get; } 
        string PluginShortType { get; }
        string PluginDescriptor { get; }
        string PluginCategory { get; }
        SelectDataKind PluginSelectDataKind { get; }
        IDictionary<string, object> DefaultValues();
        void DrawFigure(Graphics g, IDictionary<string, object> props);
        Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector);
        RectangleF SizedBoundsRect(IDictionary<string, object> props);
    }

}
