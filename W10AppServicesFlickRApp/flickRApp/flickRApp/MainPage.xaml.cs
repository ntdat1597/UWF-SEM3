namespace flickRApp
{
  using Windows.ApplicationModel;
  using Windows.ApplicationModel.DataTransfer;
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Controls;

  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
      this.txtPackageName.Text = Package.Current.Id.FamilyName;
    }

    void OnClick(object sender, RoutedEventArgs e)
    {
      DataPackage package = new DataPackage();
      package.SetText(this.txtPackageName.Text);
      Clipboard.SetContent(package);
    }
  }
}
