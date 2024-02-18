namespace HamRadioStudy.Extensions;

public static class ElementExtensions
{
    public static T? GetParentOfType<T>(this Element element) where T : Element
    {
        var parent = element.Parent;

        while (parent is not null)
        {
            if (parent is T t)
            {
                return t;
            }
            parent = parent.Parent;
        }
        return null;
    }
}
