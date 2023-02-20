using PregunFondoSur.ViewModels;

namespace PregunFondoSur.Vistas
{
    public partial class EleccionCategoria : ContentPage
    {

        #region Fields
        System.Timers.Timer timer = new System.Timers.Timer();
         
        #endregion

        #region MainPage
        public EleccionCategoria()
        {
            InitializeComponent();

            timer.Interval = 50;
            timer.Elapsed += Timer_Elapsed;
        }
        #endregion

        #region Timer_Elapsed
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                int ratation = (int)(this.circleImage.Rotation % 360);

                int value = ((ratation - 4) / 8) + 1;


            });
        }
        #endregion
        #region StartButton_Clicked
        private async void StartButton_Clicked(object sender, EventArgs e)
        {
            centerButton.IsEnabled = false;
            timer.Start();
            Rotate();
            await Task.Delay(TimeSpan.FromSeconds(1));
            StopRuleta();
        }
        #endregion
        #region StopButton_Clicked
        private async void StopRuleta()
        {
            uint duration = 1 * 1000;

            Random random = new Random();
            int no = random.Next(0, 44);

            // Se bindea el resultado del random a una propiedad del ViewModel

            var eleccionCategoriaVM = (clsEleccionCategoriaVM)BindingContext;
            eleccionCategoriaVM.CategoriaDelaRuleta = no;


            await this.circleImage.RotateTo((2 * 360) + (no * 8 + 4), duration, Easing.SinOut);
            this.timer.Stop();
            centerButton.IsEnabled = true;

        }
        #endregion

        #region Rotate
        private async void Rotate()
        {
            uint duration = 10 * 60 * 1000;

            await Task.WhenAll(
             this.circleImage.RotateTo(2200 * 360, duration)
            );
        }
        #endregion
    }
}