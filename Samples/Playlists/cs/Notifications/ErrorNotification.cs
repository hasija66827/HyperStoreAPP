using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using Microsoft.QueryStringDotNET; // QueryString.NET
using System.Reflection;

namespace SDKTemplate
{
    class ErrorNotification
    {
        // In a real app, these would be initialized with actual data
        private static string logo = "ms-appdata:///local/Andrew.jpg";
        ToastVisual visual;
        // Construct the visuals of the toast
        // Now we can construct the final toast content
        private static Dictionary<string, ErrorTitle> _Dictionary_API_Title;
        private ToastContent toastContent;
        private ToastNotification toast;
        private const int NotificationLength = 120;
        private static string RetrieveFailedForEntity = "Unfortunately!! We were unable to Get {0} from the Server.";
        private static string CreationFailedForEntity = "Unforutnately!!! {0} creation did not succeded.";
        private static string UpdationFailedForEntity = "Unforutnately!!! {0} updation did not succeded.";

        public static void PopUpHTTPGetErrorNotifcation(string APIName, string userMessage)
        {
            if (userMessage.Length > NotificationLength)
                userMessage = userMessage.Substring(0, NotificationLength);
            ErrorTitle title;
            ErrorNotification._Dictionary_API_Title.TryGetValue(APIName, out title);
            ErrorNotification errorNotification = new ErrorNotification(title?.Error_HTTPGet, userMessage);
            ToastNotificationManager.CreateToastNotifier().Show(errorNotification.toast);
        }

        public static void PopUpHTTPPostErrorNotifcation(string APIName, string userMessage)
        {
            if (userMessage.Length > NotificationLength)
                userMessage = userMessage.Substring(0, NotificationLength);
            ErrorTitle title;
            ErrorNotification._Dictionary_API_Title.TryGetValue(APIName, out title);
            ErrorNotification errorNotification = new ErrorNotification(title?.Error_HTTPPost, userMessage);
            ToastNotificationManager.CreateToastNotifier().Show(errorNotification.toast);
        }

        public static void PopUpHTTPPutErrorNotifcation(string APIName, string userMessage)
        {
            if (userMessage.Length > NotificationLength)
                userMessage = userMessage.Substring(0, NotificationLength);
            ErrorTitle title;
            ErrorNotification._Dictionary_API_Title.TryGetValue(APIName, out title);
            ErrorNotification errorNotification = new ErrorNotification(title?.Error_HTTPPut, userMessage);
            ToastNotificationManager.CreateToastNotifier().Show(errorNotification.toast);
        }


        public static void InitializeDictionary()
        {
            _Dictionary_API_Title = new Dictionary<string, ErrorTitle>();

            Type myType = typeof(API);
            PropertyInfo[] properties = myType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (PropertyInfo property in properties)
            {
                string APIName = property.GetValue(myType, null).ToString();
                if (APIName != null)
                    _Dictionary_API_Title.Add(APIName,
                        new ErrorTitle()
                        {
                            Error_HTTPGet = String.Format(RetrieveFailedForEntity, APIName),
                            Error_HTTPPost = String.Format(CreationFailedForEntity, APIName),
                            Error_HTTPPut = String.Format(UpdationFailedForEntity, APIName),
                        });
            }
        }

        public ErrorNotification(string title, string content)
        {
            visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
        {
            new AdaptiveText()
            {
                Text = title
            },

            new AdaptiveText()
            {
                Text = content
            },
        },

                    AppLogoOverride = new ToastGenericAppLogo()
                    {
                        Source = logo,
                        HintCrop = ToastGenericAppLogoCrop.Circle
                    }
                }
            };
            toastContent = new ToastContent()
            {
                Visual = visual,
            };
            // And create the toast notification
            toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddSeconds(30);
        }
    }
}
