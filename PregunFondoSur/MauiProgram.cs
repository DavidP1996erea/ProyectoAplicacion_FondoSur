namespace PregunFondoSur
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Lobster-Regular.ttf", "Lobster");
                    fonts.AddFont("SecularOne-Regular.ttf", "Secular");
                    fonts.AddFont("LilitaOne-Regular.ttf", "Lilita");
                    fonts.AddFont("FredokaOne-Regular.ttf", "Fredoka");
                    fonts.AddFont("MavenPro-VariableFont_wght.ttf", "Maven");
                });

            return builder.Build();
        }
    }
}