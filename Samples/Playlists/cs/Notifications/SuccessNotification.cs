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
    class SuccessNotification
    {
        private static Dictionary<string, SuccessTitle> _Dictionary_API_Title;

        private static string CreationSuccededForEntity = "Yay!! The {0} was created Successfully.";

        public static void InitializeDictionary()
        {
            _Dictionary_API_Title = new Dictionary<string, SuccessTitle>();
            Type myType = typeof(API);
            PropertyInfo[] properties = myType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var property in properties)
            {
                string APIName = property.GetValue(myType, null).ToString();
                if (APIName != null)
                {
                    _Dictionary_API_Title.Add(APIName,
                        new SuccessTitle()
                        {
                            Success_HTTPPost = String.Format(CreationSuccededForEntity, APIName),
                        });
                }
            }
        }

        /// <summary>
        /// It pops up the UI with the title retrieved from the dictionary(key param @APIName) and content specified by param @content.
        /// </summary>
        /// <param name="APIName">The APIName which is member of API class.</param>
        /// <param name="content">The content to be shown in notification.</param>
        public static void PopUpSuccessNotification(string APIName, string content)
        {
            SuccessTitle title;
            SuccessNotification._Dictionary_API_Title.TryGetValue(APIName, out title);
            SuccessNotification successNotification = new SuccessNotification(title?.Success_HTTPPost, content);
            ToastNotificationManager.CreateToastNotifier().Show(successNotification.toast);
        }

        private const int NotificationLength = 120;
        private static string logo = "ms-appdata:///local/Andrew.jpg";
        ToastVisual visual;
        // Construct the visuals of the toast
        // Now we can construct the final toast content
        private ToastContent toastContent;
        private ToastNotification toast;
        private SuccessNotification(string title, string content)
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
                Scenario = ToastScenario.Default
            };
            // And create the toast notification
            toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddMinutes(5);
        }
    }
}
