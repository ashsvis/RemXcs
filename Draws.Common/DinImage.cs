using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Common
{
    [Serializable]
    public class DinButton : IDrawPlugin
    {
        public string PluginShortType { get { return "DinImage"; } }
        public string PluginDescriptor { get { return "Картинка"; } }
        public string PluginCategory { get { return "Общие элементы"; } }

        public RectangleF SizedBoundsRect(IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            return new RectangleF(location, size);
        }

        public void DrawFigure(Graphics g, IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            RectangleF rect = new RectangleF(location, size);
            string pathfile = (string)props["ImagePath"];
            string filename = pathfile + (string)props["ImageName"];
            if (File.Exists(filename))
            {
                Image image;
                if (filename.ToUpper().EndsWith(".EMF"))
                    image = Metafile.FromFile(filename);
                else
                {
                    image = Bitmap.FromFile(filename);
                    if ((bool)props["Transparent"]) ((Bitmap)image).MakeTransparent();
                }
                if ((bool)props["Stretch"])
                    g.DrawImage(image, rect);
                else
                {
                    if ((bool)props["Mozaika"])
                    {
                        Size rsize = new Size(image.Width, image.Height);
                        int rowcount = (int)Math.Ceiling(rect.Height / rsize.Height);
                        int colcount = (int)Math.Ceiling(rect.Width / rsize.Width);
                        for (int row = 0; row < rowcount; row++)
                        {
                            for (int col = 0; col < colcount; col++)
                            {
                                Point p = Point.Ceiling(rect.Location);
                                p.Offset(col * rsize.Width, row * rsize.Height);
                                Rectangle r = new Rectangle(p, new Size(rsize.Width, rsize.Height));
                                Rectangle master = Rectangle.Ceiling(rect);
                                master.Intersect(r);
                                g.DrawImageUnscaledAndClipped(image, master);
                            }
                        }
                    }
                    else
                        g.DrawImage(image, rect.Location);
                }
                image.Dispose();
            }
            else
                using (Pen pen = new Pen(Color.Black))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    g.DrawRectangles(pen, new RectangleF[] { rect });
                }
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Common.Properties.Resources.DinImage; }
        }

        public IDictionary<string, object> DefaultValues()
        {
            IDictionary<string, object> props = new Dictionary<string, object>();
            props.Add("Name", String.Empty); // имя элемента
            props.Add("Plugin", PluginShortType); // имя плагина
            props.Add("Left", 0f); // x - координата
            props.Add("Top", 0f); // y - координата
            props.Add("Width", 50f); // ширина
            props.Add("Height", 20f); // высота
            props.Add("DinType", 10);
            props.Add("ImageName", String.Empty); // имя файла в папке .\images
            props.Add("ImagePath", String.Empty); // путь к папке .\images
            props.Add("Mozaika", false); // замостить
            props.Add("Stretch", false); // растянуть или сжать
            props.Add("Transparent", false); // прозрачный
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinImage(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataImages; }
        }
    }
}
