using System.Text;

namespace App.Utilities;

public class CustomStringBuilder
{
    private readonly string _oneLevelSpace;
    private readonly StringBuilder _stringBuilder;
    
    public CustomStringBuilder(string oneLevelSpace)
    {
        _oneLevelSpace = oneLevelSpace;
        _stringBuilder = new StringBuilder();
    }

    public CustomStringBuilder AppendLine()
    {
        _stringBuilder.AppendLine();

        return this;
    }

    public CustomStringBuilder AppendLine(string value, int level)
    {
        for (var i = 0; i < level; i++)
        {
            _stringBuilder.Append(_oneLevelSpace);
        }

        _stringBuilder.Append(value);
        _stringBuilder.AppendLine();

        return this;
    }

    public override string ToString()
    {
        return _stringBuilder.ToString();
    }
}