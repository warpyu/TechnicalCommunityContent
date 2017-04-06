using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Media.SpeechRecognition;

namespace Factoid.Services.Background
{
    public sealed class GeneralCommandService : IBackgroundTask
    {
        VoiceCommandServiceConnection voiceServiceConnection;
        BackgroundTaskDeferral serviceDeferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            try
            {
                voiceServiceConnection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);

                voiceServiceConnection.VoiceCommandCompleted += OnVoiceCommandCompleted;

                VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();

                var interpretation = voiceCommand.SpeechRecognitionResult.SemanticInterpretation;

                await ProcessGenerateFactAsync(interpretation);

                this.serviceDeferral.Complete();
            }
            catch (Exception ex)
            {

            }

        }

        private async Task ProcessGenerateFactAsync(SpeechRecognitionSemanticInterpretation interpretation)
        {
            await Helpers.ProgressHelper.ShowProgressScreenAsync(voiceServiceConnection, "Okay, get ready...");

            string fact = await Helpers.FactHelper.GetFactAsync();

            var destinationsContentTiles = new List<VoiceCommandContentTile>();

            var destinationTile = new VoiceCommandContentTile();

            try
            {
                destinationTile.ContentTileType = VoiceCommandContentTileType.TitleWithText;
                destinationTile.AppContext = null;
                destinationTile.AppLaunchArgument = "fact=" + fact;
                destinationTile.Title = fact;
                destinationTile.TextLine1 = "";
                destinationTile.TextLine1 = "(tap to add to favorites)";

                destinationsContentTiles.Add(destinationTile);
            }
            catch (Exception ex)
            {

            }

            VoiceCommandResponse response = VoiceCommandResponse.CreateResponse(new VoiceCommandUserMessage()
            {
                DisplayMessage = "Did you know...",
                SpokenMessage = fact

            }, destinationsContentTiles);

            await voiceServiceConnection.ReportSuccessAsync(response);
        }

        private void OnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }
    }
}
