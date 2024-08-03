using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SoupExt;

using static UnityEngine.ColorUtility;

public static class StringExt {

    public static string ToLines(this IEnumerable<string> self) => 
        string.Join(Environment.NewLine, self.Where(item => item != null));

    public static bool IsBlank(this string self) => 
        string.IsNullOrWhiteSpace(self);

    public static string ToLetters(this string self) {
        StringBuilder sb = new();
        self.ForEach(c => {
            if (char.IsLetter(c)) {
                sb.Append(c);
            }
        });
        return sb.ToString();
    }

    public static string JoinWith(
        this string self,
        string other,
        string seperator
    ) => (self.IsBlank(), other.IsBlank()) switch {
        (true, true) => null,
        (true, false) => other,
        (false, true) => self,
        (false, false) => self + seperator + other,
    };

    public static string Wrap(this string self, string pre, string post = null) =>
        pre + self + (post ?? pre);


public static string WrapML(this string self, string tag, string val = null) =>
    self.Wrap(
        (Regex.IsMatch(tag 
            ?? throw new ArgumentException("tag cannot be null"), 
        @"^[a-zA-Z0-9_-]+$") ? tag : 
            throw new ArgumentException("tag contains illegal character"))
        .Wrap("<", val is null ? ">" : 
            "=" + 
                (!(val.Contains('<') || val.Contains('>')) ? val :
                    throw new ArgumentException("val contains illegal character"))
            + ">"),
        "</" + tag + ">"
    );

public static string Format(
    this string self, 
    UnityEngine.Color? color = null, 
    bool bold = false, 
    bool italic = false, 
    uint? size = null
) => self
    .MutIf(color is not null, s => 
        s.WrapML("color", ToHtmlStringRGB(color.Value)))
    .MutIf(bold, s =>
        s.WrapML("b"))
    .MutIf(italic, s => 
        s.WrapML("i"))
    .MutIf(size is not null, s =>
        s.WrapML("size", size.Value.ToString())
    );

}
