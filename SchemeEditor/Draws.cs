using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Resources;
using System.Globalization;

[assembly: NeutralResourcesLanguageAttribute("ru")]
[assembly: CLSCompliant(true)]
namespace SchemeEditor
{
    [Serializable]
    public abstract class Draws
    {
        private bool selected = false;
        private bool changingNode = false;

        protected bool ChangingNode
        {
            get { return changingNode; }
            set { changingNode = value; }
        }        
        [NonSerialized] private object scriptObject = null;        
        [NonSerialized] private MethodInfo exeOnDoubleClick = null;
        private string scriptOnDoubleClick = "";
        [NonSerialized] private string[] scriptErrors = new string[0];
        protected Draws(): this(new Stroke(), new Fill(), new Text()) { }
        protected Draws(Stroke stroke, Fill fill, Text text)
        {
            this.Text = text;
            this.Stroke = stroke;
            this.Fill = fill;
            this.Angle = 0.0F;
            // нулевой индекс для обозначения тела фигуры, для перемещения
            this.MarkerIndex = 0; 
        }
        public virtual void Initialize()
        {
            if (this.Stroke == null) this.Stroke = new Stroke();
            if (this.Fill == null) this.Fill = new Fill();
            if (this.Text == null) this.Text = new Text();
            if (this.scriptErrors == null) this.scriptErrors = new string[0];
            if (this.scriptOnDoubleClick == null) this.scriptOnDoubleClick = "";
        }
        public Boolean Selected 
        {
            get { return (selected); }
            set { selected = value; if (!selected) changingNode = false; } 
        }
        public Text Text { get; set; }
        public Stroke Stroke { get; set; }
        public Fill Fill { get; set; }
        public Single Angle {get; set;}
        public Single ScaleX { get; set; }
        public Single ScaleY { get; set; }
        public int MarkerIndex { get; set; }
        public bool NodeChanging 
        {
            get { return (changingNode); }
            set { SetNodeChanging(value); } 
        }
        public string[] ScriptErrors() { return (scriptErrors); }
        public string ScriptOnDoubleClick 
        {
            get { return (scriptOnDoubleClick); }
            set
            {
                scriptOnDoubleClick = value;
                if (!String.IsNullOrEmpty(value))
                {
                    using (Microsoft.CSharp.CSharpCodeProvider codeProvider =
                        new Microsoft.CSharp.CSharpCodeProvider())
                    {
                        CompilerParameters compilerParameters = new CompilerParameters();
                        compilerParameters.GenerateInMemory = true;
                        compilerParameters.IncludeDebugInformation = true;
                        compilerParameters.ReferencedAssemblies.Add("System.dll");
                        compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
                        compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                        compilerParameters.ReferencedAssemblies.Add("SchemeEditor.exe");
                        string[] lines = new string[1] { scriptOnDoubleClick };
                        CompilerResults compilerResults =
                            codeProvider.CompileAssemblyFromSource(compilerParameters, lines);
                        if (compilerResults.Errors.Count > 0)
                        {
                            // Display compilation errors.
                            scriptObject = null;
                            exeOnDoubleClick = null;
                            scriptErrors = new string[compilerResults.Errors.Count];
                            for (int i = 0; i < compilerResults.Errors.Count; i++)
                            {
                                CompilerError ce = compilerResults.Errors[i];
                                scriptErrors[i] = String.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) {3}",
                                    ce.ErrorNumber, ce.Line, ce.Column, ce.ErrorText);
                            }
                        }
                        else
                        {
                            scriptErrors = new string[0];
                            scriptObject = null;
                            exeOnDoubleClick = null;
                            // получаем количество классов в сборке, 
                            // в нашем случае это всего один класс
                            Type[] assembleTypes = compilerResults.CompiledAssembly.GetTypes();
                            if (assembleTypes.Length == 1)
                            {   // список параметров и их типов для вызова конструктора 
                                // с одним параметром
                                Type[] constructorParametersTypes = new Type[1] { this.GetType() };
                                ConstructorInfo typeConstructor =
                                    assembleTypes[0].GetConstructor(constructorParametersTypes);
                                if (typeConstructor != null)
                                {   // один параметр, передаёт ссылку на себя
                                    object[] objectParameters = new object[1] { this };
                                    scriptObject = typeConstructor.Invoke(objectParameters);
                                    exeOnDoubleClick = assembleTypes[0].GetMethod("DoubleClick");
                                    scriptErrors = new string[1] { "Нет ошибок компиляции." };
                                }
                                else
                                    scriptErrors = new string[1] { "Конструктор класса не был создан." };
                            }
                            else
                                scriptErrors = new string[1] { "Нет классов для выполнения." };
                        }
                    }
                }
            }
        }
        public bool OnDoubleClickExecute()
        {
            bool result = false;
            if (exeOnDoubleClick == null) ScriptOnDoubleClick = scriptOnDoubleClick;
            if (scriptObject != null)
            {
                if (exeOnDoubleClick != null)
                {
                    try
                    {   // нет параметров у функции
                        object[] methodParameters = new object[] { }; 
                        exeOnDoubleClick.Invoke(scriptObject, methodParameters);
                        result = true;
                        scriptErrors = new string[1] {"Выполненено успешно."};
                    }
                    catch (TargetParameterCountException e)
                    {
                        result = false;
                        scriptErrors = new string[1] {e.Message};
                    }
                }
                else
                    scriptErrors = new string[1] {"Нет функции для выполнения."};
            }
            else
                scriptErrors = new string[1] {"Нет объекта для выполнения функции."};
            return (result);
        }
        protected virtual void SetNodeChanging(bool value)
        {
            changingNode = value;
        }
        public virtual bool CanNodeChanging()
        {
            return (true);
        }
        public abstract void DrawFigure(Graphics graphics);
        public abstract void DrawFocusFigure(Graphics graphics, Pen pen, PointF pnt, int markerIndex);
        public abstract void MoveUp(Single stepMove);
        public abstract void MoveDown(Single stepMove);
        public abstract void MoveLeft(Single stepMove);
        public abstract void MoveRight(Single stepMove);
        public abstract void Offset(PointF point);
        public abstract Boolean PointInFigure(PointF point);
        public virtual RectangleF GetBounds { get { return RectangleF.Empty; } }
        public abstract void UpdateSize(PointF offset);
        public abstract void DeleteFigureNode(int markerIndex);
        public abstract void InsertFigureNode(PointF pnt);
        public abstract PointF[] GetPoints();
        public abstract void AddPointsRange(PointF[] pts);
        public abstract void FlipVertical();
        public abstract void FlipHorizontal();
        public abstract void Rotate(Single angle);
        public abstract void RotateAt(Single angle, Single cx, Single cy);
        public virtual RectangleF CalcFocusRect(PointF pnt)
        {
            RectangleF rect = GetBounds;
            Single dx = pnt.X;
            Single dy = pnt.Y;
            Single dw = dx;
            Single dh = dy;
            switch (MarkerIndex)
            {
                case 0: // перемещение фигуры
                    rect.X += dx;
                    rect.Y += dy;
                    break;
                case 1: if ((rect.Height - dh > 0) && (rect.Width - dw > 0)) // влево-вверх
                    {
                        rect.Width -= dw;
                        rect.Height -= dh;
                        rect.X += dx;
                        rect.Y += dy;
                    }
                    break;
                case 2: if (rect.Height - dh > 0) // вверх
                    {
                        rect.Height -= dh;
                        rect.Y += dy;
                    }
                    break;
                case 3: if ((rect.Height - dh > 0) && (rect.Width + dw > 0)) // вправо-вверх
                    {
                        rect.Width += dw;
                        rect.Height -= dh;
                        rect.Y += dy;
                    }
                    break;
                case 4: if (rect.Width + dw > 0) // вправо
                    {
                        rect.Width += dw;
                    }
                    break;
                case 5: if ((rect.Width + dw > 0) && (rect.Height + dh > 0)) // вправо-вниз
                    {
                        rect.Width += dw;
                        rect.Height += dh;
                    }
                    break;
                case 6: if (rect.Height + dh > 0) // вниз
                    {
                        rect.Height += dh;
                    }
                    break;
                case 7: if ((rect.Height + dh > 0) && (rect.Width - dw > 0))// влево-вниз
                    {
                        rect.Width -= dw;
                        rect.Height += dh;
                        rect.X += dx;
                    }
                    break;
                case 8: if (rect.Width - dw > 0) // влево
                    {
                        rect.Width -= dw;
                        rect.X += dx;
                    }
                    break;
            }
            return (rect);
        }
        public bool MarkerSelected(PointF pt)
        {
            bool found = false;
            this.MarkerIndex = 0;
            if (this.Selected)
            {
                if (!NodeChanging)
                {
                    RectangleF[] rects = GetBoundMarkers(GetBounds);
                    for (int i = 0; i < rects.Length; i++)
                    {
                        if (rects[i].Contains(pt))
                        {
                            found = true;
                            this.MarkerIndex = i + 1;
                            break;
                        }
                    }
                }
                if (NodeChanging && !found)
                {
                    RectangleF[] rects = this.GetNodeMarkers();
                    for (int i = 0; i < rects.Length; i++)
                    {
                        if (rects[i].Contains(pt)) 
                        { 
                            found = true;
                            this.MarkerIndex = -(i + 1);
                            break;
                        }
                    }
                }
            }
            return (found);
        }
        public abstract void AddGraphicsContent(GraphicsPath gp, RectangleF rect);
        public abstract RectangleF[] GetNodeMarkers();
        public virtual RectangleF[] GetBoundMarkers(RectangleF rect)
        {
            if (rect.Width <= 10) rect.Inflate(5, 0);
            if (rect.Height <= 10) rect.Inflate(0, 5);
            PointF[] pts = new PointF[8];
            pts[0].X = rect.Left;                     pts[0].Y = rect.Top;
            pts[1].X = rect.Left + rect.Width * 0.5F; pts[1].Y = rect.Top;
            pts[2].X = rect.Right;                    pts[2].Y = rect.Top;
            pts[3].X = rect.Right;                    pts[3].Y = rect.Top + rect.Height * 0.5F;
            pts[4].X = rect.Right;                    pts[4].Y = rect.Bottom;
            pts[5].X = rect.Left + rect.Width * 0.5F; pts[5].Y = rect.Bottom;
            pts[6].X = rect.Left;                     pts[6].Y = rect.Bottom;
            pts[7].X = rect.Left;                     pts[7].Y = rect.Top + rect.Height * 0.5F;
            rect.Inflate(-5, -5);
            RectangleF[] rects = new RectangleF[pts.Length];
            Single k;
            for (int i = 0; i < pts.Length; i++)
            {
                if ((rect.Width <= 5) && ((i == 1) || (i == 5))) 
                    k = 1;
                else if ((rect.Height <= 5) && ((i == 3) || (i == 7))) 
                    k = 1;
                else 
                    k = 3;
                rects[i] = RectangleF.FromLTRB(pts[i].X - k, pts[i].Y - k,
                                               pts[i].X + k, pts[i].Y + k);
            }
            return (rects);
        }
        public bool IsRotated()
        {
            return (Math.Abs(this.Angle) > 0.0001F);
        }
        public bool IsScaled()
        {
            return (Math.Abs(this.ScaleX - 1.0F) > 0.0001F) || 
                   (Math.Abs(this.ScaleY - 1.0F) > 0.0001F);
        }
        public bool IsTransformed()
        {
            return (IsRotated() || IsScaled());
        }
    }

    [Serializable]
    public class AllDraws: List<Draws>
    {
        public void Offset(PointF point) { foreach (Draws drw in this) if (drw.Selected) drw.Offset(point); }
        public void MoveUp(Single step) { foreach (Draws drw in this) if (drw.Selected) drw.MoveUp(step); }
        public void MoveDown(Single step) { foreach (Draws drw in this) if (drw.Selected) drw.MoveDown(step); }
        public void MoveLeft(Single step) { foreach (Draws drw in this) if (drw.Selected) drw.MoveLeft(step); }
        public void MoveRight(Single step) { foreach (Draws drw in this) if (drw.Selected) drw.MoveRight(step); }
        public Draws PointInFigure(PointF point)
        {
            Draws found = null;
            Draws drw;
            // Проверка нажатия на маркерах
            for (int i = this.Count - 1; i >= 0; i--)
            {
                drw = (Draws)this[i];
                if (drw.MarkerSelected(point)) { found = drw; break; }
            }
            if (found == null)
            {
                // Проверка нажатия на теле фигур
                for (int i = this.Count - 1; i >= 0; i--)
                {
                    drw = (Draws)this[i];
                    if (drw.PointInFigure(point)) { found = drw; break; }
                }
            }
            return (found);
        }
        public void UnselectAll() { foreach (Draws drw in this) drw.Selected = false; }
   
        public new int Add(Draws value)
        {
            Draws drw = value as Draws;
            if (drw != null)
            {
                drw.Selected = true;
                base.Add(value);
            }
            return this.Count - 1;
        }
        public new void Clear() { UnselectAll(); base.Clear(); }
        public bool FigureInList(Draws fig)
        { 
            bool result = false; 
            foreach (Draws drw in this) if (drw == fig) { result = true; break; }
            return (result);
        }
        public void DrawFocusFigures(Graphics graphics, Pen pen, PointF pnt, int markerIndex)
        {
            foreach (Draws drw in this) drw.DrawFocusFigure(graphics, pen, pnt, markerIndex);
        }
        public void UpdateSize(PointF lastPoint)
        {
            foreach (Draws drw in this) drw.UpdateSize(lastPoint);
        }
    }
}
