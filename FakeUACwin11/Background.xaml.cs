using System.Windows;

namespace FakeUACwin11;

public partial class Background : Window
{
    public Background(Screen screen)
    {
        InitializeComponent();

        // Assurez-vous que le screen est valide
        if (screen == null)
        {
            throw new ArgumentNullException(nameof(screen), "Le paramètre screen ne peut pas être null.");
        }

        // Configurer la fenêtre pour s'afficher en plein écran fenêtré sur l'écran spécifié
        this.WindowStyle = WindowStyle.None; // Retirer les bordures
        this.ResizeMode = ResizeMode.NoResize; // Empêcher le redimensionnement
        this.WindowStartupLocation = WindowStartupLocation.Manual; // Définir manuellement la position
        this.Left = screen.Bounds.Left;
        this.Top = screen.Bounds.Top;
        this.Width = screen.Bounds.Width;
        this.Height = screen.Bounds.Height;
        
    }
    
    void Win_Click(object sender, RoutedEventArgs e)
    {
#if DEBUG
        Close();
#endif
    }
}