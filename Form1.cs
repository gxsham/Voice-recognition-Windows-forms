using System;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.Media;

namespace voice_recognition 
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speechsynth = new SpeechSynthesizer();
        SpeechRecognitionEngine receng = new SpeechRecognitionEngine();
        Choices choice = new Choices();

        public Form1()
        {
            InitializeComponent();
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            startbtn.Enabled = false;
            stopbtn.Enabled = true;
            choice.Add(new string[] { "dorin", "close", "da", "net", "valera" , "what is the current time", "open page" });
            Grammar gr = new Grammar(new GrammarBuilder(choice));
            try
            {
                receng.RequestRecognizerUpdate();
                receng.LoadGrammar(gr);
                receng.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(receng_SpeechRecognized);
                receng.SetInputToDefaultAudioDevice();
                receng.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        void receng_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "da":
                    SoundPlayer simpleSound = new SoundPlayer(@"c:\da.wav");
                    simpleSound.Play();
                    break;

                case "net":
                    SoundPlayer simpleSound2 = new SoundPlayer(@"c:\net.wav");
                    simpleSound2.Play();
                    break;

                case "valera":
                    SoundPlayer simpleSound3 = new SoundPlayer(@"c:\valera.wav");
                    simpleSound3.Play();                
                    break;

                case "dorin":
                    SoundPlayer simpleSound4 = new SoundPlayer(@"c:\dorin.wav");
                    simpleSound4.Play();
                    break;

                case "what is the current time":
                    speechsynth.SpeakAsync("right now it is " + DateTime.Now.ToLongTimeString());
                    break;
                case "open page":
                    Process.Start("chrome", "https://www.facebook.com/dorin.balan.1");
                    break;
                case "close":
                    speechsynth.Speak("Fuck off bitch");
                    Application.Exit();
                    break;

            }
            list1.Items.Add(e.Result.Text.ToString());
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            receng.RecognizeAsyncStop();
            startbtn.Enabled = true;
            stopbtn.Enabled = false;
        }

    
    }
}