namespace flickRAppBackgroundTask
{
  using Windows.ApplicationModel.AppService;
  using Windows.ApplicationModel.Background;
  using Windows.Foundation.Collections;
  using System;
  using System.Net.Http;
  using Windows.Storage;
  using System.IO;
  using Windows.ApplicationModel.DataTransfer;

  public sealed class BackgroundTask : IBackgroundTask
  {
    BackgroundTaskDeferral deferral;

    public async void Run(IBackgroundTaskInstance taskInstance)
    {
      this.deferral = taskInstance.GetDeferral();
      taskInstance.Canceled += OnCancelled;

      var appService = taskInstance.TriggerDetails as AppServiceTriggerDetails;

      if (appService != null)
      {
        // we should do more validation here
        if (appService.Name == "flickrphoto")
        {
          appService.AppServiceConnection.RequestReceived += OnAppServiceRequestReceived;
        }
      }
      else
      {
        this.deferral.Complete();
        this.deferral = null;
      }
    }
    async void OnAppServiceRequestReceived(AppServiceConnection sender,
      AppServiceRequestReceivedEventArgs args)
    {
      var localDeferral = args.GetDeferral();
      var parameters = args.Request.Message;
      ValueSet vs = new ValueSet();

      if (parameters.ContainsKey("query"))
      {
        var result = await FlickrSearcher.SearchAsync((string)parameters["query"]);

        if ((result != null) && (result.Count > 0))
        {
          var first = result[0];
          HttpClient client = new HttpClient();
          using (var response = await client.GetAsync(new Uri(first.ImageUrl)))
          {
            var file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(first.Id.ToString(),
              CreationCollisionOption.ReplaceExisting);

            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
              await response.Content.CopyToAsync(fileStream);
            }
            var token = SharedStorageAccessManager.AddFile(file);
            vs["pictureToken"] = token;
          }
        }
      }
      await args.Request.SendResponseAsync(vs);
      localDeferral.Complete();
    }

    void OnCancelled(IBackgroundTaskInstance sender,
      BackgroundTaskCancellationReason reason)
    {
      this.deferral.Complete();
    }
  }
}
