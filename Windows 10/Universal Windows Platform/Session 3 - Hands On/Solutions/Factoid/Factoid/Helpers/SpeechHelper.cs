using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace Factoid.Helpers
{
    public static  class SpeechHelper
    {
        public async static Task ReadFactAsync(string fact)
        {
            var synthesizer = new SpeechSynthesizer();

            synthesizer.Voice = SpeechSynthesizer.AllVoices.Where(w => w.Gender == VoiceGender.Female && w.Language.Equals(System.Globalization.CultureInfo.CurrentUICulture.Name)).FirstOrDefault();

            SpeechSynthesisStream stream = await synthesizer.SynthesizeTextToStreamAsync(fact);

            var mediaElement = new MediaElement();
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

            return;
        }

        public async static Task InitializeVoiceCommandsAsync(bool updatePhraseLists = true)
        {
            try
            {
                StorageFile commandsFile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"FactoidVoiceCommands.xml");

                try
                {
                    await VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(commandsFile);
                }
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {

            }

            return;
        }
    }
}
