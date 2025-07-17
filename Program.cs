
using Gtk;
using Gdk;

class Program
{
    static void Main()
    {
        var app = Application.New("ua.org.accounting.window", Gio.ApplicationFlags.FlagsNone);

        app.OnActivate += (_, args) =>
        {
            GridWindow firstWindow = new(app);
            firstWindow.Show();
        };

        //Css
        {
            string styleDefaultFile = Path.Combine(AppContext.BaseDirectory, "Default.css");
            var displayDefault = Display.GetDefault();

            if (File.Exists(styleDefaultFile) && displayDefault != null)
            {
                CssProvider provider = CssProvider.New();
                provider.LoadFromPath(styleDefaultFile);
                StyleContext.AddProviderForDisplay(displayDefault, provider, 800);
            }
        }

        app.RunWithSynchronizationContext(null);
    }
}
