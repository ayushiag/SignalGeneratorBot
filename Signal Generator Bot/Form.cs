using System;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace Signal_Generator_Bot
{


   [Serializable]
   public class ProfileForm
   {
      
      //[Prompt(" Are you looking for some interesting signals? {||}")]
      [Pattern(@"^([yYesnNo]?)$")]
      public string Response;
      
      public string  LastName;
      [Prompt("What is your company? {||}")]
      public string Company;
      [Template(TemplateUsage.NotUnderstood, "Sorry , \"{0}\" Not avilable .", "Try again, I don't get \"{0}\".")]
      public string LunchFood;
      public static IForm<ProfileForm> BuildForm()
      {
         return new FormBuilder<ProfileForm>().Message("Hey :)").Message("I am a Signal Generator Bot!").Message("Are you looking for signal generation?").Build();
      } 
   }
   }
   [Serializable]
   public enum Gender
   {
      Male = 1, Female = 2
   };
