﻿namespace ClientApp
{
  using System;
  using Windows.ApplicationModel.AppService;
  using Windows.ApplicationModel.DataTransfer;
  using Windows.Foundation.Collections;
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Controls;
  using Windows.UI.Xaml.Media.Imaging;

  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
      this.Loaded += OnLoaded;
    }
    void OnLoaded(object sender, RoutedEventArgs e)
    {
      Clipboard.ContentChanged += OnClipboardChanged;
      this.OnClipboardChanged(null, null);
    }
    async void OnClipboardChanged(object sender, object e)
    {
      var packageView = Clipboard.GetContent();

      if (packageView != null)
      {
        try
        {
          var text = await packageView.GetTextAsync();

          if (!string.IsNullOrEmpty(text))
          {
            this.txtPackageName.Text = text;
          }
        }
        catch
        {
        }
      }
    }
    async void OnClick(object sender, RoutedEventArgs e)
    {
      AppServiceConnection connection = new AppServiceConnection();
      connection.PackageFamilyName = this.txtPackageName.Text;
      connection.AppServiceName = this.txtServiceName.Text;

      var status = await connection.OpenAsync();

      if (status == AppServiceConnectionStatus.Success)
      {
        ValueSet parameters = new ValueSet();
        parameters["query"] = this.txtQuery.Text;

        var response = await connection.SendMessageAsync(parameters);

        if (response.Status == AppServiceResponseStatus.Success)
        {
          var token = response.Message["pictureToken"] as string;

          if (!string.IsNullOrEmpty(token))
          {
            var file = await SharedStorageAccessManager.RedeemTokenForFileAsync((string)token);

            BitmapImage bitmap = new BitmapImage();

            using (var fileStream = await file.OpenReadAsync())
            {
              await bitmap.SetSourceAsync(fileStream);
            }
            this.image.Source = bitmap;
          }
        }
        connection.Dispose();
      }
    }
  }
}
