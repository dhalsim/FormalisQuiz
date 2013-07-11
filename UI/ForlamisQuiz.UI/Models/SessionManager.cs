using System.Web;
using FormalisQuiz.Models;

namespace ForlamisQuiz.UI.Models
{
    public class SessionManager
    {
        private const string USER_CONTEXT = "UserContext";

        public static UserContext UserContext
        {
            get
            {
                return Get<UserContext>(USER_CONTEXT);
            }
            set
            {
                Add(USER_CONTEXT, value);
            }
        }

        private static void Add(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        private static T Get<T>(string key)
        {
            var obj = HttpContext.Current.Session[key];
            if (obj == null || !(obj is T))
            {
                return default(T);
            }

            return (T)obj;
        } 
    }
}