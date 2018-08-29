using System;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace Signal_Generator_Bot
{
   [Serializable]
   public class SCPI
   {
      float freq;
      float amplitude;


      public static string BuildSCPI()
      {
         //return new FormBuilder<ProfileForm>().Message("I am SIGNAL GENERATOR BOT :)").OnCompletion(async (context, profileForm) => {
         //   await context.PostAsync("Your profile is complete.");
         //}).Build();
         string waveform = "Hey Waveform";
         return waveform;

      }
   }
}

