namespace HamRadioStudy.Views;

public class StackedProgressBar : ContentView
{
    public static readonly BindableProperty Value1Property =
        BindableProperty.Create(nameof(Value1), typeof(double), typeof(StackedProgressBar), 0.0, propertyChanged: Redraw);

    public static readonly BindableProperty Value2Property =
        BindableProperty.Create(nameof(Value2), typeof(double), typeof(StackedProgressBar), 0.0, propertyChanged: Redraw);

    public static readonly BindableProperty MaximumProperty =
        BindableProperty.Create(nameof(Maximum), typeof(double), typeof(StackedProgressBar), 100.0, propertyChanged: Redraw);

    public static readonly BindableProperty Color1Property =
        BindableProperty.Create(nameof(Color1), typeof(Color), typeof(StackedProgressBar), Colors.LightGreen);

    public static readonly BindableProperty Color2Property =
        BindableProperty.Create(nameof(Color2), typeof(Color), typeof(StackedProgressBar), Colors.IndianRed);

    public static readonly BindableProperty Color3Property =
        BindableProperty.Create(nameof(Color3), typeof(Color), typeof(StackedProgressBar), Colors.Transparent);

    public double Value1
    {
        get => (double)GetValue(Value1Property);
        set => SetValue(Value1Property, value);
    }

    public double Value2
    {
        get => (double)GetValue(Value2Property);
        set => SetValue(Value2Property, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public Color Color1
    {
        get => (Color)GetValue(Color1Property);
        set => SetValue(Color1Property, value);
    }

    public Color Color2
    {
        get => (Color)GetValue(Color2Property);
        set => SetValue(Color2Property, value);
    }

    public Color Color3
    {
        get => (Color)GetValue(Color3Property);
        set => SetValue(Color3Property, value);
    }

    private readonly Grid _progressBarGrid = new ();

    public StackedProgressBar()
    {
        Content = _progressBarGrid;
        Redraw();
    }

    private static void Redraw(BindableObject bindable, object oldvalue, object newvalue)
    {
        (bindable as StackedProgressBar)?.Redraw();
    }

    private void Redraw()
    {
        _progressBarGrid.Children.Clear();
        _progressBarGrid.ColumnDefinitions.Clear();

        if (Maximum <= 0) return;

        var totalValue = Value1 + Value2;
        if (totalValue > Maximum)
        {
            throw new InvalidOperationException("The sum of Value1 and Value2 cannot exceed Maximum");
        }
        var percentage1 = Value1 / Maximum;
        var percentage2 = Value2 / Maximum;
        var percentage3 = (Maximum - totalValue) / Maximum;

        _progressBarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(percentage1, GridUnitType.Star) });
        _progressBarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(percentage2, GridUnitType.Star) });
        _progressBarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(percentage3, GridUnitType.Star) });

        _progressBarGrid.Add(new BoxView { Color = Color1 }, 0, 0);
        _progressBarGrid.Add(new BoxView { Color = Color2 }, 1, 0);
        _progressBarGrid.Add(new BoxView { Color = Color3 }, 2, 0);
    }
}
