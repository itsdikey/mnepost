namespace CGPost.Controls;

public partial class StatusOfPackageView : ContentView
{
    public static readonly BindableProperty IsLastProperty = 
        BindableProperty.Create(nameof(IsLast), typeof(bool), typeof(StatusOfPackageView));

    public bool IsLast
    {
        get { return (bool)GetValue(IsLastProperty); }
        set { SetValue(IsLastProperty, value); }
    }
    public StatusOfPackageView()
    {
        InitializeComponent();
        Loaded += StatusOfPackageView_Loaded;
    }

    private void StatusOfPackageView_Loaded(object sender, EventArgs e)
    {

        if (IsLast)
        {
            GraphicsView.Drawable = new FinalDrawable();
        }
        else
        {
            GraphicsView.Drawable = new PendingDrawable();
        }


    }

    public class PendingDrawable : IDrawable
    {
        static Color _lightGray = Color.FromArgb("#CCCCCC");
        static Color _darkGray = Color.FromArgb("#767676");
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.LightGray;
            canvas.StrokeSize = 2;
            canvas.DrawLine(dirtyRect.Left + dirtyRect.Width / 2, dirtyRect.Bottom, dirtyRect.Left + dirtyRect.Width / 2, dirtyRect.Top);
            canvas.StrokeSize = 0;
            canvas.FillColor = Colors.LightGray;
            canvas.FillCircle(dirtyRect.Center, 8);
            canvas.FillColor = Colors.DarkGray;
            canvas.FillCircle(dirtyRect.Center, 6);
        }
    }

    public class FinalDrawable : IDrawable
    {
        static Color _lightBlue = Color.FromArgb("#B5CAFD");
        static Color _darkBlue = Color.FromArgb("#658DFE");
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            
            canvas.StrokeColor = _lightBlue;
            canvas.StrokeSize = 2;
            canvas.DrawLine(dirtyRect.Center.X, dirtyRect.Bottom, dirtyRect.Center.X, dirtyRect.Center.Y);
            canvas.StrokeSize = 0;
            canvas.FillColor = _lightBlue;
            canvas.FillCircle(dirtyRect.Center, 8);
            canvas.FillColor = _darkBlue;
            canvas.FillCircle(dirtyRect.Center, 6);
        }
    }
}