using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;

namespace Signal_Generator_Bot
{
   [BotAuthentication]
   public class MessagesController : ApiController
   {
      /// <summary>
      /// POST: api/Messages
      /// Receive a message from a user and reply to it
      /// </summary>
      /// 

      //internal static IDialog<ProfileForm> MakeRootDialog()
      //{
      //   return Chain.From(() => FormDialog.FromForm(ProfileForm.BuildForm));
      //}

      internal static IDialog<ProfileForm> MakeRootDialog()
      {
         return Chain.From(() => FormDialog.FromForm(ProfileForm.BuildForm))
           .Do(async (context, order) =>
           {
              try
              {
                 var completed = await order;
              // See if debug reached this and call our sendSCPI() method here
              await context.PostAsync(SCPI.BuildSCPI());
              }
              catch (FormCanceledException<ProfileForm> e)
              {
                 string reply;
                 if (e.InnerException == null)
                 {
                    reply = $"You quit on {e.Last} -- maybe you can finish next time!";
                 }
                 else
                 {
                    reply = "Sorry, I've had a short circuit. Please try again.";
                 }
                 await context.PostAsync(reply);
              }
           });

      }
      public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
      {
         // Detect if this is a Message activity  
         if (activity.Type == ActivityTypes.Message)
         {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            // Call our FormFlow by calling MakeRootDialog  
            await Conversation.SendAsync(activity, MakeRootDialog);
            //string input = (activity.Text ?? string.Empty).ToUpper();
            //if(input.Contains("WAVEFORM"))
            //   {
            //   Activity reply = activity.CreateReply("We will display you the sine wave");
            //   await connector.Conversations.ReplyToActivityAsync(reply);
            //}

         }
         else
         {
            // This was not a Message activity  
            HandleSystemMessage(activity);
         }
         // Return response  
         var response = Request.CreateResponse(HttpStatusCode.OK);
         return response;
      }

      private Activity HandleSystemMessage(Activity message)
      {
         if (message.Type == ActivityTypes.DeleteUserData)
         {
            // Implement user deletion here
            // If we handle user deletion, return a real message
         }
         else if (message.Type == ActivityTypes.ConversationUpdate)
         {
            // Handle conversation state changes, like members being added and removed
            // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
            // Not available in all channels
         }
         else if (message.Type == ActivityTypes.ContactRelationUpdate)
         {
            // Handle add/remove from contact lists
            // Activity.From + Activity.Action represent what happened
         }
         else if (message.Type == ActivityTypes.Typing)
         {
            // Handle knowing tha the user is typing
         }
         else if (message.Type == ActivityTypes.Ping)
         {
         }

         return null;
      }
   }
}