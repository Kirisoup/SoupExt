namespace SoupExt;

public static class EnumEx {
    public static void ForEach<T>(this IEnumerable<T> self, Action<T> action) {
        foreach (var item in self ?? throw new ArgumentNullException(nameof(self))) {
            (action ?? throw new ArgumentNullException(nameof(action)))(item);
        }
    }

    public static T[] Singlet<T>(this T self) => new T[] { self };
}

